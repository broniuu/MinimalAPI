namespace MinimalAPI;

public interface IPageService
{
    PageParameters SetPageParameters(string patternOfpageSize, string patternOfpageNumber, HttpContext http);

    int SetPageSize(string patternOfpageSize, HttpContext http);

    int SetPageNumber(string patternOfPageNumber, HttpContext http);
}