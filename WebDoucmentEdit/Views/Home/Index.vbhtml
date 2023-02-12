@Code
    ViewData("Title") = "Home Page"
End Code

@section css
    <style type="text/css">
        #pdf_container {
            background: #ccc;
            text-align: center;
            display: none;
            padding: 5px;
            height: 820px;
            overflow: auto;
        }
    </style>
End Section
<p style="color : red"> @ViewBag.Message</p>
<div class="row" style="margin-top : 20px">
    <div class="col-md-6">
        @Using (Html.BeginForm("Index", "Home", FormMethod.Post, New With {.enctype = "multipart/form-data", .class = "form-inline"}))
            @<div class="form-group">
                @Html.TextBox("file", "", New With {.type = "file", .class = "form-control", .accept = ".doc, .docx, .pdf"})
            </div>
            @<div class="form-group">
                <input type="submit" value="Upload..." class="btn btn-primary" />
            </div>

        End Using
    </div>
</div>

<div class="row" style="margin-top : 20px">
    
    <div class="col-md-12">
        @If ViewBag.Url <> "" Then
            @<iframe src="/web/viewer.html?file=@ViewBag.Url" width="100%" height="2000px" style="border: none" />
        End If

    </div>
</div>

@section scripts

End Section