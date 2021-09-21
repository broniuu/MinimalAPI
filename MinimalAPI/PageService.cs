﻿
namespace MinimalAPI;
public class PageService : IPageService
{
    public PageParameters SetPageParameters(string patternOfpageSize, string patternOfpageNumber, HttpContext http)
    {       
        var pageSize = SetPageSize(patternOfpageSize, http);
        var pageNumber = SetPageNumber(patternOfpageNumber, http);
        return new PageParameters()
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    public int SetPageSize(string patternOfpageSize, HttpContext http)
    {
        int pageSize;
        try
        {
            pageSize = Int32.Parse(http.Request.Query[patternOfpageSize].ToString());
        }
        catch (FormatException)
        {
            return 0;
        }
        return pageSize;
    }

    public int SetPageNumber(string patternOfPageNumber, HttpContext http)
    {
        int pageNumber;
        try
        {
            pageNumber = Int32.Parse(http.Request.Query[patternOfPageNumber].ToString());
        }
        catch (FormatException)
        {
            return 0;
        }
        return pageNumber;
    }
}