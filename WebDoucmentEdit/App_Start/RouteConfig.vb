Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports System.Web.Routing

Public Module RouteConfig
    Public Sub RegisterRoutes(ByVal routes As RouteCollection)
        routes.IgnoreRoute("{resource}.axd/{*pathInfo}")

        routes.MapRoute(
            name:="Default",
            url:="{controller}/{action}/{id}",
            defaults:=New With {.controller = "Home", .action = "Index", .id = UrlParameter.Optional}
        )
        'routes.Add("web", New Route("web", New WebRouteHandler()))

        'routes.Add("build", New Route("build", New BuildRouteHandler()))
        routes.MapRoute("web", "web/{*any}")
        routes.MapRoute("build", "build/{*any}")

        RouteTable.Routes.RouteExistingFiles = False
    End Sub
End Module

Friend Class BuildRouteHandler
    Implements IRouteHandler

    Public Function GetHttpHandler(requestContext As RequestContext) As IHttpHandler Implements IRouteHandler.GetHttpHandler
        Dim filename = requestContext.RouteData.Values("build")
        If String.IsNullOrEmpty(filename) Then
            requestContext.HttpContext.Response.End()
        Else
            requestContext.HttpContext.Response.Clear()
            Dim filepath = requestContext.HttpContext.Server.MapPath("~/build/" + filename)
            Try
                requestContext.HttpContext.Response.WriteFile(filepath)
            Catch ex As Exception
                requestContext.HttpContext.Response.End()
            End Try
            requestContext.HttpContext.Response.End()
        End If
        Return Nothing

    End Function



End Class

Friend Class WebRouteHandler
    Implements IRouteHandler

    Public Function GetHttpHandler(requestContext As RequestContext) As IHttpHandler Implements IRouteHandler.GetHttpHandler
        Dim filename = requestContext.RouteData.Values("web")
        If String.IsNullOrEmpty(filename) Then
            requestContext.HttpContext.Response.End()
        Else
            requestContext.HttpContext.Response.Clear()
            Dim filepath = requestContext.HttpContext.Server.MapPath("~/web/" + filename)
            Try
                requestContext.HttpContext.Response.WriteFile(filepath)
            Catch ex As Exception
                requestContext.HttpContext.Response.End()
            End Try
            requestContext.HttpContext.Response.End()
        End If
        Return Nothing
    End Function
End Class
