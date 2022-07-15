using System.Text.RegularExpressions;
using API.Models;
using Newtonsoft.Json;
using RestSharp;

namespace API.BLL;

public class BLL_BNTDatabase
{
    public object GetBNTDatabase()
    {
        const string url = "https://www.bibliotheque.nat.tn/BNT/Portal/Recherche/Search.svc/Search";
        var client = new RestClient(url);
        var request = new RestRequest();
        var results = new List<Book>();

        for (int i = 0; i < 3; i++)
        {
            var body = @"{
" + "\n" +
                       @"    ""query"": {
" + "\n" +
                       @"        ""CloudTerms"": [],
" + "\n" +
                       @"        ""FacetFilter"": ""{}"",
" + "\n" +
                       @"        ""ForceSearch"": true,
" + "\n" +
                       @"        ""InitialSearch"": false,
" + "\n" +
                       @"        ""Page"": " + i + @",
" + "\n" +
                       @"        ""PageRange"": 3,
" + "\n" +
                       @"        ""QueryGuid"": ""768f8222-4296-42e6-b395-53928ab2f7f2"",
" + "\n" +
                       @"        ""QueryString"": ""*:*"",
" + "\n" +
                       @"        ""ResultSize"": 50,
" + "\n" +
                       @"        ""ScenarioCode"": ""BNTPARTOUT"",
" + "\n" +
                       @"        ""ScenarioDisplayMode"": ""display-standard"",
" + "\n" +
                       @"        ""SearchGridFieldsShownOnResultsDTO"": [],
" + "\n" +
                       @"        ""SearchLabel"": ""جميع الوثائق"",
" + "\n" +
                       @"        ""SearchTerms"": """",
" + "\n" +
                       @"        ""SortField"": ""YearOfPublication_int_sort"",
" + "\n" +
                       @"        ""SortOrder"": 0,
" + "\n" +
                       @"        ""TemplateParams"": {
" + "\n" +
                       @"            ""Scenario"": """",
" + "\n" +
                       @"            ""Scope"": ""BNT"",
" + "\n" +
                       @"            ""Size"": null,
" + "\n" +
                       @"            ""Source"": """",
" + "\n" +
                       @"            ""Support"": """",
" + "\n" +
                       @"            ""UseCompact"": false
" + "\n" +
                       @"        },
" + "\n" +
                       @"        ""UseSpellChecking"": null,
" + "\n" +
                       @"        ""Url"": ""https://www.bibliotheque.nat.tn/BNT/search.aspx?SC=BNTPARTOUT&QUERY=Les+raisins+de+la+domination.+Une+histoire+sociale+de+l%E2%80%99alcool+en+Tunisie+a+l%E2%80%99epoque+du+Protectorat+%281881-1956%29+&QUERY_LABEL=#/Search/(query:(CloudTerms:!(),FacetFilter:%7B%7D,ForceSearch:!t,InitialSearch:!f,Page:5,PageRange:50,QueryGuid:'768f8222-4296-42e6-b395-53928ab2f7f2',QueryString:'*:*',ResultSize:100,ScenarioCode:BNTPARTOUT,ScenarioDisplayMode:display-standard,SearchGridFieldsShownOnResultsDTO:!(),SearchLabel:'%D8%AC%D9%85%D9%8A%D8%B9%20%D8%A7%D9%84%D9%88%D8%AB%D8%A7%D8%A6%D9%82',SearchTerms:'',SortField:YearOfPublication_int_sort,SortOrder:0,TemplateParams:(Scenario:'',Scope:BNT,Size:!n,Source:'',Support:'',UseCompact:!f),UseSpellChecking:!n),sst:4)""
" + "\n" +
                       @"    },
" + "\n" +
                       @"    ""sst"": 4
" + "\n" +
                       @"}";
            request.AddJsonBody(body);
            RestResponse response = client.Post(request);
            // deserialize the response body to json object
            if (response.Content != null)
            {
                var json = JsonConvert.DeserializeObject<dynamic>(response.Content);

                var fields = json.d.Results;

                foreach (var item in fields)
                {
                    // todo: need to fix this
                    string s = item.Resource.Id.ToString();
                    var isbn = s.Length > 5 && s.StartsWith("isbn:")
                        ? s.Substring(5)
                        : "";


                    var rscId = item.Resource.RscId;
                    Book book = new Book();
                    {
                        book.Title = item.Resource.Ttl;
                        book.Author = item.Resource.Crtr;
                        book.Cover =
                            "https://www.bibliotheque.nat.tn/Ils/digitalCollection/DigitalCollectionThumbnailHandler.ashx?documentId=" +
                            rscId +
                            "&size=LARGE&fallback=https%3a%2f%2fwww.bibliotheque.nat.tn%2fui%2fskins%2fBNT%2fportal%2ffront%2fimages%2fGeneral%2fDocType%2fMONO_LARGE.png";
                        book.ISBN = isbn;
                        book.Date = item.Resource.Dt;
                        book.Publisher = item.Resource.Pbls;
                        book.Subject = item.Resource.Subj != null ? item.Resource.Subj : "";
                        book.Type = item.Resource.Type;
                        DAL.Insert.InsertBook(book);
                    }
                }
            }
        }

        return results;
    }
}