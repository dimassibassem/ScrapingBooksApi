using System.Text;
using API.Models.DAL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace API.Models.BLL;

public class BllBntDatabase
{
    public async Task<JObject> GetBNTDatabase()
    {
        var obj = new JObject();
        const string url = "https://www.bibliotheque.nat.tn/BNT/Portal/Recherche/Search.svc/Search";
        for (int i = 0; i < 3; i++)
        {
            // var client = new RestClient(url);
            HttpClient client = new HttpClient();
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
                       @"    ""sst"":4
" + "\n" +
                       @"}";
            // var request = new RestRequest();
            // request.AddJsonBody(body);
            // var postData = new List<KeyValuePair<string, string>>()
            // {
            //     new("query", body)
            // };

            // var content = body.ToString();
            //
            // var response = await client.PostAsync(url, body);
            var response = await client.PostAsync(url, new StringContent(body, Encoding.UTF8, "application/json"));
            var responseString = "";

            if (response.IsSuccessStatusCode)
            {
                responseString = await response.Content.ReadAsStringAsync();
                obj = JObject.Parse(responseString);
            }


            if (responseString != "")
            {
                var json = JsonConvert.DeserializeObject<dynamic>(responseString);

                var fields = json.d.Results;

                foreach (var item in fields)
                {
                    string s = item.Resource.Id.ToString();
                    var isbn = s.Length > 5 && s.StartsWith("isbn:")
                        ? s.Substring(5)
                        : "";


                    var rscId = item.Resource.RscId;
                    Book book = new Book();
                    {
                        book.Title = item.Resource.Ttl;
                        book.Author = item.Resource.Crtr != null ? item.Resource.Crtr : "";
                        book.Cover =
                            "https://www.bibliotheque.nat.tn/Ils/digitalCollection/DigitalCollectionThumbnailHandler.ashx?documentId=" +
                            rscId +
                            "&size=LARGE&fallback=https%3a%2f%2fwww.bibliotheque.nat.tn%2fui%2fskins%2fBNT%2fportal%2ffront%2fimages%2fGeneral%2fDocType%2fMONO_LARGE.png";
                        book.ISBN = isbn;
                        book.Date = item.Resource.Dt != null ? item.Resource.Dt : "";
                        book.Publisher = item.Resource.Pbls != null ? item.Resource.Pbls : "";
                        book.Subject = item.Resource.Subj != null ? item.Resource.Subj : "";
                        book.Type = item.Resource.Type != null ? item.Resource.Type : "";
                    }
                    var b = DalBook.GetBook(book.ISBN, book.Title);
                    //check if book is already in database
                    if (b.Id == null)
                    {
                        DalBook.InsertBook(book);
                    }
                }
            }
        }

        return obj;
    }
}