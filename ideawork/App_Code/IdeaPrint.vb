Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO
Imports System.Configuration.ConfigurationManager

Public Class IdeaPrint

    Public doc As New Document()
    Public wri As PdfWriter
    Public cb As PdfContentByte
    Public tb As PdfPTable
    Public erro As String = ""
    Public fontstandard As iTextSharp.text.Font
    Public fontstandardbold As iTextSharp.text.Font
    Public fontTdHd As iTextSharp.text.Font
    Public fontTd As iTextSharp.text.Font
    Public fontTdBold As iTextSharp.text.Font
    Public fontTdSmall As iTextSharp.text.Font
    Public fontbase As BaseFont
    Public fontbasebold As BaseFont
    Public fontsizestandard As Integer = 9
    Public fontsizestandardsmall As Integer = 7
    Public pdfNomeFile As String = ""
    Dim TableParagraph As Paragraph
    Dim cartellasito As String = ""
    Const convmm As Double = 0.353
    Public pxtable As Integer = 0
    Public pytable As Integer = 0
    Public Class HeaderInfo
        Public nome As String = ""
        Public perclargura As Integer = 0
        Sub New(n As String, p As Integer)
            nome = n
            perclargura = p
        End Sub
    End Class
    Public Sub adderr(txerro As String)
        erro = erro & txerro & vbCrLf
    End Sub

    Sub New(nomefile As String, Optional margem As Integer = 5, Optional nomefont As String = "Tahoma", Optional sizefont As Integer = 9)
        Try
            erro = ""
            margem = margem / convmm
            cartellasito = AppSettings("CartellaSito")
            doc = New Document(iTextSharp.text.PageSize.A4, margem, margem, margem, margem)
            wri = PdfWriter.GetInstance(doc, New FileStream(nomefile, FileMode.Create))
            pdfNomeFile = nomefile
            doc.Open()
            LoadFonts(nomefont, sizefont)
            cb = wri.DirectContent

        Catch ex As Exception
            adderr(ex.Message)
        End Try
    End Sub
    Public Sub LoadFonts(nomefont As String, Optional sizefontst As Integer = 0)
        Try
            If sizefontst = 0 Then sizefontst = fontsizestandard
            fontstandard = FontFactory.GetFont(nomefont, sizefontst, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)
            fontstandardbold = FontFactory.GetFont(nomefont, sizefontst, iTextSharp.text.Font.BOLD, BaseColor.BLACK)
            fontTd = FontFactory.GetFont(nomefont, sizefontst, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)
            fontTdBold = FontFactory.GetFont(nomefont, sizefontst, iTextSharp.text.Font.BOLD, BaseColor.BLACK)
            fontTdSmall = FontFactory.GetFont(nomefont, fontsizestandardsmall, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY)
            fontTdHd = FontFactory.GetFont(nomefont, sizefontst, iTextSharp.text.Font.BOLD, BaseColor.WHITE)
            fontbase = BaseFont.CreateFont(cartellasito & "\css\fonts\" & nomefont & ".ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED)
        Catch ex As Exception
            adderr(ex.Message)
        End Try
    End Sub
    Public Function CloseSave() As String
        Try
            doc.Close()
            wri.Close()
        Catch ex As Exception
            adderr(ex.Message)
        End Try
        Return erro
    End Function

    Public Sub addParagrafo(tx As String)
        Try

            Dim p As New Paragraph(tx)
            p.Alignment = Element.ALIGN_JUSTIFIED
            p.SpacingAfter = 5
            p.SpacingBefore = 5
            doc.Add(p)
        Catch ex As Exception
            adderr(ex.Message)
        End Try
    End Sub

    Public Sub AddImage(fileimg As String, px As Double, py As Double, dx As Double, dy As Double)
        Try
            Dim im As Image
            im = Image.GetInstance(fileimg)
            im.ScaleAbsolute(dx / convmm, dy / convmm)
            im.SetAbsolutePosition(px / convmm, doc.PageSize.Height - py / convmm - dy / convmm)
            doc.Add(im)
        Catch ex As Exception
            'adderr(ex.Message) ' faz nada so não inclui
        End Try
    End Sub
    Public Sub addline(xi As Double, yi As Double, xf As Double, yf As Double)
        Try
            cb.MoveTo(xi / convmm, doc.PageSize.Height - yi / convmm)
            cb.LineTo(xf / convmm, doc.PageSize.Height - yf / convmm)
            cb.Stroke()
        Catch ex As Exception
            adderr(ex.Message)
        End Try
    End Sub
    Public Sub AddRetangule(xi As Double, yi As Double, dx As Double, dy As Double)
        Try
            cb.Rectangle(xi / convmm, doc.PageSize.Height - yi / convmm, dx / convmm, -dy / convmm)
            cb.Stroke()
        Catch ex As Exception
            adderr(ex.Message)
        End Try
    End Sub
    Public Sub AddCircle(xc As Double, yc As Double, diam As Double)
        Try
            cb.Circle(xc / convmm, doc.PageSize.Height - yc / convmm, diam / convmm)
            cb.Stroke()
        Catch ex As Exception
            adderr(ex.Message)
        End Try
    End Sub
    Public Sub AddText(tx As String, px As Double, py As Double, Optional fontsize As Integer = 0, Optional isbold As Boolean = False, Optional textalignx As String = "L")
        Try
            If IsNothing(tx) = False Then
                If fontsize = 0 Then fontsize = fontsizestandard
                cb.SetFontAndSize(fontbase, fontsize)
                cb.BeginText()

                ' cb.SetTextMatrix(px / convmm, doc.PageSize.Height - py / convmm)
                Dim typealign As Integer = Element.ALIGN_LEFT
                If textalignx = "C" Then
                    textalignx = Element.ALIGN_CENTER
                ElseIf textalignx = "R" Then
                    textalignx = Element.ALIGN_RIGHT
                End If
                cb.ShowTextAligned(typealign, tx, px / convmm, doc.PageSize.Height - py / convmm, 0)
                cb.EndText()
                cb.SetFontAndSize(fontbase, fontsizestandard)
            End If
        Catch ex As Exception
            adderr(ex.Message)
        End Try
    End Sub
    Public Sub TableStart(header As String, px As Integer, py As Integer, Optional largmm As Integer = 0)
        Try
            ' estrutura header (nomecoluna1;perc largura1 vbtab nomecoluna2;perc largura2 vbtab ....)
            Dim cc() As String = Split(header, ",")
            Dim hi() As HeaderInfo = {}
            ReDim Preserve hi(0)
            Dim i As Integer = 0
            Dim lperctable As Integer = 0


            If largmm > 0 Then
                lperctable = ((largmm / convmm) / doc.PageSize.Width) * 100 ' Table size is set to 100% of the page
            Else
                lperctable = 100
            End If
            Dim ltotaltable As Single = lperctable * doc.PageSize.Width / 100
            Dim lcols() As Single = {}
            For i = 0 To UBound(cc)
                If Len(cc(i)) > 0 Then
                    ReDim Preserve hi(i)
                    ReDim Preserve lcols(i)
                    Dim cc1() As String = Split(cc(i), ";")
                    ReDim Preserve cc1(1)
                    Dim lcoluna As Single = (cc1(1) / 100) * largmm / convmm
                    hi(i) = New HeaderInfo(cc1(0), CInt(lcoluna))
                    lcols(i) = CSng(lcoluna)
                End If
            Next
            Dim numcol As Integer = UBound(hi) + 1
            tb = New PdfPTable(numcol)
            tb.SplitLate = False
            tb.WidthPercentage = lperctable
            pxtable = px / convmm
            pytable = doc.PageSize.Height - py
            tb.HorizontalAlignment = 1 'Left aLign    
            tb.SpacingBefore = 0 'px / convmm
            tb.SpacingAfter = 0


            Dim espaco As New PdfPCell()
            espaco.Colspan = numcol
            espaco.FixedHeight = py / convmm
            espaco.Border = 0

            tb.AddCell(espaco)
            tb.SetWidths(lcols) ' Set the column widths on table creation. Unlike HTML cells cannot be sized.


            For i = 0 To numcol - 1
                Dim ce As New PdfPCell(New Phrase(hi(i).nome, fontTdHd))
                ce.HorizontalAlignment = Element.ALIGN_CENTER
                ce.BackgroundColor = New BaseColor(64, 64, 64)
                tb.AddCell(ce)
            Next


            For i = 0 To tb.NumberOfColumns - 1
                tb.AbsoluteWidths(i) = lcols(i)
            Next
        Catch ex As Exception
            adderr(ex.Message)
        End Try
    End Sub
    Public Function getTableHeight() As Single
        Dim h As Single = 0
        Dim r As Integer = 0
        For r = 0 To tb.Rows.Count - 1
            h = h + tb.Rows(r).GetCells(0).MinimumHeight
        Next
        Return h
    End Function
    Public Sub TableStartWhithoutHeader(lcols() As Integer, px As Integer, py As Integer, Optional largmm As Integer = 0)
        Try
            ' estrutura header (nomecoluna1;perc largura1 vbtab nomecoluna2;perc largura2 vbtab ....)
            tb = New PdfPTable(lcols.Length)
            pxtable = px / convmm
            pytable = doc.PageSize.Height - py
            Dim lperctable As Single = 0
            If largmm > 0 Then
                lperctable = ((largmm / convmm) / doc.PageSize.Width) * 100 ' Table size is set to 100% of the page
            Else
                lperctable = 100
            End If
            Dim i As Integer = 0
            Dim ltotaltable As Single = lperctable * doc.PageSize.Width / 100

            For i = 0 To UBound(lcols)
                Dim lcoluna As Single = (lcols(0) / 100) * largmm / convmm
                lcols(i) = CSng(lcoluna)
            Next
            Dim numcol As Integer = UBound(lcols) + 1
            tb = New PdfPTable(numcol)
            tb.WidthPercentage = lperctable
            pxtable = px / convmm
            pytable = doc.PageSize.Height - py
            tb.HorizontalAlignment = 1 'Left aLign    
            tb.SpacingBefore = 0 'px / convmm
            tb.SpacingAfter = 0


            Dim espaco As New PdfPCell()
            espaco.Colspan = lcols.Length
            espaco.FixedHeight = py / convmm
            espaco.Border = 0

            tb.AddCell(espaco)
            tb.SetWidths(lcols) ' Set the column widths on table creation. Unlike HTML cells cannot be sized.



            For i = 0 To tb.NumberOfColumns - 1
                tb.AbsoluteWidths(i) = lcols(i)
            Next
        Catch ex As Exception
            adderr(ex.Message)
        End Try
    End Sub

    Public Sub TableAddLine(tableline() As String, Optional small As Boolean = False)
        Try
            ' estrutura tablinee (campo1 vbtab campo2 ...)
            For i = 0 To tb.NumberOfColumns - 1
                Dim tl() As String = Split(tableline(i), ";")
                ReDim Preserve tl(5)
                Dim tipofont As Font = fontTd
                If small = True Then
                    tipofont = fontTdSmall
                End If
                If tl(2) = "B" Then
                    tipofont = fontTdBold
                End If

                Select Case UCase(tl(1))
                    Case "L"
                        Dim ce As New PdfPCell(New Phrase(tl(0), tipofont))
                        ce.Padding = 5
                        ce.HorizontalAlignment = Element.ALIGN_LEFT

                        tb.AddCell(ce)
                    Case "C"
                        Dim ce As New PdfPCell(New Phrase(tl(0), tipofont))
                        ce.Padding = 5
                        ce.HorizontalAlignment = Element.ALIGN_CENTER
                        tb.AddCell(ce)
                    Case "R"
                        Dim ce As New PdfPCell(New Phrase(tl(0), tipofont))
                        ce.Padding = 5
                        ce.HorizontalAlignment = Element.ALIGN_RIGHT
                        tb.AddCell(ce)
                    Case "LPAR"
                        Dim ce As New PdfPCell(New Paragraph(tl(0), tipofont))
                        ce.Padding = 5
                        ce.HorizontalAlignment = Element.ALIGN_LEFT
                        tb.AddCell(ce)
                    Case "LPARTITLEBOLD"
                        Dim par As New Phrase()
                        Dim ll() As String = Split(tl(0), vbCrLf)
                        Dim j As Integer = 0
                        For j = 0 To UBound(ll)
                            If ll(j) <> "" Then
                                If j <= 1 Then
                                    par.Add(New Chunk(ll(j) & vbCrLf & vbCrLf, fontTdBold))
                                Else
                                    par.Add(New Chunk(ll(j) & vbCrLf, fontTdSmall))
                                End If
                            End If
                        Next
                        Dim ce As New PdfPCell(par)
                        ce.Padding = 5
                        ce.HorizontalAlignment = Element.ALIGN_LEFT
                        tb.AddCell(ce)
                    Case "IMG"
                        Try
                            Dim img As Image = iTextSharp.text.Image.GetInstance(tl(0))
                            Dim coef As Double = img.Width / img.Height
                            If tl(3) <> "" Then
                                Dim newheight As Single = CInt(tl(3) / convmm)
                                Dim newwidth As Single = CInt(newheight * (img.Width / img.Height))
                                If newheight < newwidth Then
                                    newwidth = CInt(tl(3) / convmm)
                                    newheight = CInt(newwidth * (img.Height / img.Width))
                                End If
                                img.ScaleAbsolute(newwidth, newheight)
                            End If
                            img.Border = 0
                            img.Alignment = Element.ALIGN_CENTER
                            Dim ce As New PdfPCell(img)

                            ce.Padding = 10
                            ce.HorizontalAlignment = Element.ALIGN_CENTER
                            ce.AddElement(img)
                            tb.AddCell(ce)
                        Catch ex As Exception
                            ' deixa passar
                            Dim ce As New PdfPCell(New Phrase("No Foto", tipofont))
                            ce.Padding = 5
                            tb.AddCell(ce)
                        End Try

                    Case Else
                End Select


            Next
        Catch ex As Exception
            adderr(ex.Message)
        End Try
    End Sub
    Public Sub TableAddCell(textcontent As String, Optional TypeAndAlignment As String = "L", Optional small As Boolean = False, Optional bold As Boolean = False, Optional maximgdimension As Integer = 0, Optional haveborder As Boolean = False)
        Try
            ' estrutura tablinee (campo1 vbtab campo2 ...)

            Dim tipofont As Font = fontTd
            If small = True Then
                tipofont = fontTdSmall
            End If
            If bold = True Then
                tipofont = fontTdBold
            End If

            Select Case UCase(TypeAndAlignment)
                Case "L"
                    Dim ce As New PdfPCell(New Phrase(textcontent, tipofont))
                    ce.Padding = 5
                    If haveborder = False Then
                        ce.Border = 0
                    End If
                    ce.HorizontalAlignment = Element.ALIGN_LEFT

                    tb.AddCell(ce)
                Case "C"
                    Dim ce As New PdfPCell(New Phrase(textcontent, tipofont))
                    ce.Padding = 5
                    If haveborder = False Then
                        ce.Border = 0
                    End If
                    ce.HorizontalAlignment = Element.ALIGN_CENTER
                    tb.AddCell(ce)
                Case "R"
                    Dim ce As New PdfPCell(New Phrase(textcontent, tipofont))
                    ce.Padding = 5
                    If haveborder = False Then
                        ce.Border = 0
                    End If
                    ce.HorizontalAlignment = Element.ALIGN_RIGHT
                    tb.AddCell(ce)
                Case "LPAR"
                    Dim ce As New PdfPCell(New Paragraph(textcontent, tipofont))
                    ce.Padding = 5
                    If haveborder = False Then
                        ce.Border = 0
                    End If
                    ce.HorizontalAlignment = Element.ALIGN_LEFT
                    tb.AddCell(ce)
                Case "LPARTITLEBOLD"
                    Dim par As New Phrase()
                    Dim ll() As String = Split(textcontent, vbCrLf)
                    Dim j As Integer = 0
                    For j = 0 To UBound(ll)
                        If ll(j) <> "" Then
                            If j = 0 Then
                                par.Add(New Chunk(ll(j) & vbCrLf & vbCrLf, fontTdBold))
                            Else
                                par.Add(New Chunk(ll(j) & vbCrLf, fontTdSmall))
                            End If
                        End If
                    Next
                    Dim ce As New PdfPCell(par)
                    ce.Padding = 5
                    If haveborder = False Then
                        ce.Border = 0
                    End If
                    ce.HorizontalAlignment = Element.ALIGN_LEFT
                    tb.AddCell(ce)
                Case "IMG"
                    Try
                        Dim img As Image = iTextSharp.text.Image.GetInstance(textcontent)
                        Dim coef As Double = img.Width / img.Height
                        If maximgdimension > 0 Then
                            Dim newheight As Single = CInt(maximgdimension / convmm)
                            Dim newwidth As Single = CInt(newheight * (img.Width / img.Height))
                            If newheight < newwidth Then
                                newwidth = CInt(maximgdimension / convmm)
                                newheight = CInt(newwidth * (img.Height / img.Width))
                            End If
                            img.ScaleAbsolute(newwidth, newheight)
                        End If
                        img.Border = 0
                        img.Alignment = Element.ALIGN_CENTER
                        Dim ce As New PdfPCell(img)
                        If haveborder = False Then
                            ce.Border = 0
                        End If
                        ce.Padding = 10
                        ce.HorizontalAlignment = Element.ALIGN_CENTER
                        ce.AddElement(img)
                        tb.AddCell(ce)
                    Catch ex As Exception
                        ' deixa passar
                        Dim ce As New PdfPCell(New Phrase("No Foto", tipofont))
                        ce.Padding = 5
                        If haveborder = False Then
                            ce.Border = 0
                        End If
                        tb.AddCell(ce)
                    End Try

                Case Else
            End Select



        Catch ex As Exception
            adderr(ex.Message)
        End Try
    End Sub

    Public Sub TableEnd(Optional tabelasequencial As Boolean = False)
        Try
            If tabelasequencial = True Then
                doc.Add(tb)
            Else
                tb.SetTotalWidth(tb.AbsoluteWidths)
                Dim pcb As PdfContentByte = wri.DirectContent
                tb.WriteSelectedRows(0, -1, pxtable, pytable, pcb)
            End If

        Catch ex As Exception
            adderr(ex.Message)
        End Try
    End Sub
End Class
