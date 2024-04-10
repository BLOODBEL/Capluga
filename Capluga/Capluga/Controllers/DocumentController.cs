using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using ClosedXML.Excel;
using System.Data;
using Capluga.Entities;
using DocumentFormat.OpenXml.EMMA;
using Microsoft.Ajax.Utilities;
using Capluga.Models;
using iTextSharp.text.pdf.draw;

namespace Capluga.Controllers
{
    public class DocumentController : Controller
    {
        CarritoModel modelCarrito = new CarritoModel();

        [HttpPost]
        public ActionResult DescargarPDF(int masterPurchaseID)
        {
            // Suponemos que esta llamada obtiene los datos necesarios
            List<FacturaEnt> facturas = modelCarrito.ConsultaDetalleFactura(masterPurchaseID);

            if (facturas == null || !facturas.Any())
            {
                return Content("No se encontraron datos para generar la factura.");
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                Document documento = new Document(PageSize.A4);
                PdfWriter writer = PdfWriter.GetInstance(documento, memoryStream);
                documento.Open();

                var primeraFactura = facturas.FirstOrDefault();

                if (primeraFactura != null)
                {
                    // Definir fuentes
                    Font fontTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, BaseColor.BLACK);
                    Font fontSubtitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.BLACK);
                    Font fontCuerpo = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK);

                    // Agregar un logo
                    string imageURL = Server.MapPath("~/Images/Logo.png"); // Asegúrate de tener esta imagen en tu proyecto
                    Image logo = Image.GetInstance(imageURL);
                    logo.ScaleToFit(50f, 50f);
                    logo.SpacingBefore = 10;
                    logo.SpacingAfter = 10;
                    logo.Alignment = Element.ALIGN_RIGHT;
                    documento.Add(logo);

                    // Título de la factura
                    documento.Add(new Paragraph("Factura", fontTitulo));

                    // Línea separadora
                    LineSeparator separator = new LineSeparator(1f, 100f, BaseColor.DARK_GRAY, Element.ALIGN_CENTER, -1);
                    documento.Add(new Chunk(separator));

                    // Datos de la factura
                    documento.Add(new Paragraph($"Factura #: {primeraFactura.MasterPurchaseID}", fontSubtitulo));
                    documento.Add(new Paragraph($"Fecha: {primeraFactura.PurchaseDate.ToString("dd/MM/yyyy")}", fontCuerpo));
                    documento.Add(new Paragraph($"Cliente ID: {primeraFactura.UserName}", fontCuerpo));
                    documento.Add(new Paragraph("\n"));

                    // Crear una tabla para los detalles de la factura
                    PdfPTable table = new PdfPTable(new float[] { 3, 1, 2, 1, 2 }) { WidthPercentage = 100 };
                    table.DefaultCell.Padding = 5;

                    // Definir el estilo del encabezado de la tabla
                    BaseColor colorEncabezado = new BaseColor(0, 121, 182); // Un azul oscuro
                    Font fontEncabezado = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.WHITE);

                    // Añadir los encabezados de la tabla
                    PdfPCell cell = new PdfPCell(new Phrase("Nombre del Producto", fontEncabezado)) { BackgroundColor = colorEncabezado };
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Cantidad", fontEncabezado)) { BackgroundColor = colorEncabezado };
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Precio Unitario", fontEncabezado)) { BackgroundColor = colorEncabezado };
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Impuesto", fontEncabezado)) { BackgroundColor = colorEncabezado };
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Subtotal", fontEncabezado)) { BackgroundColor = colorEncabezado };
                    table.AddCell(cell);

                    // Rellenar la tabla con los detalles de la factura
                    foreach (var item in facturas)
                    {
                        table.AddCell(new Phrase(item.Name, fontCuerpo));
                        table.AddCell(new Phrase(item.PaidQuantity.ToString(), fontCuerpo));
                        table.AddCell(new Phrase(item.PaidPrice.ToString("C"), fontCuerpo));
                        table.AddCell(new Phrase(item.Tax.ToString("C"), fontCuerpo));
                        table.AddCell(new Phrase(item.SubTotal.ToString("C"), fontCuerpo));
                    }

                    // Añadir la tabla al documento
                    documento.Add(table);

                    // Total general de la factura
                    documento.Add(new Paragraph("Total: " + facturas.Sum(x => x.Total).ToString("C"), fontTitulo));

                    documento.Close();
                }

                byte[] bytes = memoryStream.ToArray();
                return File(bytes, "application/pdf", $"Factura_{masterPurchaseID}.pdf");
            }
        }


        [HttpPost]
        public ActionResult ExportarAExcel(int masterPurchaseID)
        {
            List<FacturaEnt> facturas = modelCarrito.ConsultaDetalleFactura(masterPurchaseID);

            if (facturas == null || !facturas.Any())
            {
                return Content("No se encontraron datos para generar el Excel.");
            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Factura");
                var primeraFactura = facturas.FirstOrDefault();

                if (primeraFactura != null)
                {
                    // Establecer el estilo de la hoja de cálculo
                    var facturaStyle = workbook.Style;
                    facturaStyle.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    facturaStyle.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    facturaStyle.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    // Agregar datos de factura
                    worksheet.Cell("A1").Value = $"Factura: {primeraFactura.MasterPurchaseID}";
                    worksheet.Cell("A2").Value = $"Fecha: {primeraFactura.PurchaseDate.ToString("dd/MM/yyyy")}";
                    worksheet.Cell("A3").Value = $"Cliente ID: {primeraFactura.UserName}";

                    // Estilo para los datos de la factura
                    worksheet.Range("A1:A3").Style = facturaStyle;
                    worksheet.Range("A1:A3").Style.Font.Bold = true;
                    worksheet.Range("A1:A3").Style.Font.FontSize = 12;
                    worksheet.Range("A1:A3").Style.Fill.BackgroundColor = XLColor.WhiteSmoke;

                    int currentRow = 5; // Comenzar la tabla de ítems un poco más abajo

                    // Encabezados de la tabla de ítems
                    worksheet.Cell(currentRow, 1).Value = "Nombre del Producto";
                    worksheet.Cell(currentRow, 2).Value = "Cantidad";
                    worksheet.Cell(currentRow, 3).Value = "Precio Unitario";
                    worksheet.Cell(currentRow, 4).Value = "Impuesto";
                    worksheet.Cell(currentRow, 5).Value = "Subtotal";

                    // Rellenar los ítems de la factura
                    foreach (var item in facturas)
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = item.Name;
                        worksheet.Cell(currentRow, 2).Value = item.PaidQuantity;
                        worksheet.Cell(currentRow, 3).Value = item.PaidPrice;
                        worksheet.Cell(currentRow, 4).Value = item.Tax;
                        worksheet.Cell(currentRow, 5).Value = item.SubTotal;
                    }

                    // Estilo para las celdas de la tabla de ítems
                    var rangoTabla = worksheet.Range($"A5:E{currentRow}");
                    rangoTabla.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    rangoTabla.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                    rangoTabla.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    rangoTabla.Style.Font.FontSize = 10;

                    // Estilo para las celdas de totales
                    worksheet.Cell(currentRow + 1, 4).Value = "Total:";
                    worksheet.Cell(currentRow + 1, 5).Value = facturas.Sum(x => x.Total);
                    worksheet.Range($"A{currentRow + 1}:E{currentRow + 1}").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range($"A{currentRow + 1}:E{currentRow + 1}").Style.Fill.BackgroundColor = XLColor.WhiteSmoke;
                    worksheet.Range($"A{currentRow + 1}:E{currentRow + 1}").Style.Font.Bold = true;

                    // Ajustar ancho de las columnas
                    worksheet.Columns(1, 5).AdjustToContents();

                    using (MemoryStream stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Factura_{masterPurchaseID}.xlsx");
                    }
                }

                return Content("No se encontraron detalles de factura.");
            }
        }



    }
}
