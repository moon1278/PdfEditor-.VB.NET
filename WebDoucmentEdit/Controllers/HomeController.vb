Imports System.IO
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Word

Public Class HomeController
    Inherits System.Web.Mvc.Controller

    Function Index() As ActionResult
        ViewData("dvWord") = ""
        Return View()
    End Function

    <HttpPost>
    Function Index(ByVal file As HttpPostedFileBase) As ActionResult
        Dim randomName As String = DateTime.Now.Ticks.ToString
        Dim directoryPath As String = Server.MapPath("~/web/") & randomName + "_files/"

        Try
            If file.ContentLength > 0 Then
                Dim fileSavePath As Object = directoryPath & Path.GetFileName(file.FileName)
                'If Directory not present, create it.
                If Not Directory.Exists(Server.MapPath("~/web/")) Then
                    Directory.CreateDirectory(Server.MapPath("~/web/"))
                End If
                If Not Directory.Exists(directoryPath) Then
                    Directory.CreateDirectory(directoryPath)
                End If
                file.SaveAs(fileSavePath)
                ViewBag.File = fileSavePath
                'ViewBag.Url = randomName + "_files/" & Path.GetFileName(file.FileName)
                ViewBag.Url = "file:///C:/Users/Administrator/Documents/invoice_2001321.pdf"

                Session("FilePath") = fileSavePath
            End If
            ViewBag.Message = "File Uploaded Successfully!"
            Return View()
        Catch ex As Exception
            ViewBag.Message = "FileUploaded Failed"
            Return View()
        End Try
    End Function

    Function About() As ActionResult
        ViewData("Message") = "Your application description page."

        Return View()
    End Function
    <HttpPost>
    Function UploadFilePdf() As String
        Dim data = Request.BinaryRead(Request.ContentLength)
        If data.Length = 0 Then
            Return "Blob Data Error"
        End If
        Dim filepath = Session("FilePath")
        If System.IO.File.Exists(filepath) Then
            System.IO.File.Delete(filepath)
        End If

        Using stream As FileStream = New FileStream(filepath, FileMode.Create)
            stream.Write(data, 0, data.Length)
        End Using

        Return "Successfully Save the PDF"

    End Function

    Function Contact() As ActionResult
        ViewData("Message") = "Your contact page."

        Return View()
    End Function
End Class
