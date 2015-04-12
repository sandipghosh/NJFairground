
namespace NJFairground.Web.DTO.ResponseDto
{
    using NJFairground.Web.DTO.Base;
    using NJFairground.Web.Models;
    using NJFairground.Web.Utilities;
    using System.Collections.Generic;

    public class MapResponseDto : ResponseBase
    {
        public MapResponseDto()
        {
            MapUrl = CommonUtility.ResolveServerUrl(string.Format("{0}{1}",
                CommonUtility.GetAppSetting<string>("UploadFolderItemImagePath"), "NJ_Fairground_guide_map.jpg"), false);
            MapCoordinates = new List<MapCoordinateModel>()
            {
                new MapCoordinateModel(){Title="Blood Mobile",Shape="rect", Coords="271,531,297,554"},
                new MapCoordinateModel(){Title="Performing Arts tent",Shape="rect", Coords="498,532,524,555"},
                new MapCoordinateModel(){Title="FIRST AID (in Admin. Bldg.)",Shape="rect", Coords="446,588,472,611"},
                new MapCoordinateModel(){Title="Family Entertainment Area [Circus Hollywood, racing pigs, petting zoo]",Shape="rect", Coords="534,549,560,572"},
                new MapCoordinateModel(){Title="INFORMATION BOOTHS",Shape="rect", Coords="682,483,708,506"},
                new MapCoordinateModel(){Title="Wheelchairs/scooters",Shape="rect", Coords="670,461,696,484"},
                new MapCoordinateModel(){Title="Christmas Tree Exhibit",Shape="rect", Coords="628,470,654,493"},
                new MapCoordinateModel(){Title="Newton Rotary Food Booth",Shape="rect", Coords="627,450,653,473"},
                new MapCoordinateModel(){Title="Oxen Display",Shape="rect", Coords="574,457,600,480"},
                new MapCoordinateModel(){Title="Winegarden",Shape="rect", Coords="524,458,550,481"},
                new MapCoordinateModel(){Title="RESTROOMS",Shape="rect", Coords="488,463,514,486"},
                new MapCoordinateModel(){Title="ATM",Shape="rect", Coords="489,440,515,463"},
                new MapCoordinateModel(){Title="Rare Heritage Breeds Education Center",Shape="rect", Coords="461,456,487,479"},
                new MapCoordinateModel(){Title="ATM",Shape="rect", Coords="370,466,396,489"},
                new MapCoordinateModel(){Title="Admissions/Ticket Office",Shape="rect", Coords="344,465,370,488"},
                new MapCoordinateModel(){Title="STEAM Robotics",Shape="rect", Coords="350,446,376,469"},
                new MapCoordinateModel(){Title="INFORMATION BOOTHS",Shape="rect", Coords="266,468,292,491"},
                new MapCoordinateModel(){Title="New Jersey Hall of Fame Exhibit",Shape="rect", Coords="245,456,271,479"},
                new MapCoordinateModel(){Title="Community Service Tent",Shape="rect", Coords="311,420,337,443"},
                new MapCoordinateModel(){Title="Craft Tent",Shape="rect", Coords="347,423,394,446"},
                new MapCoordinateModel(){Title="Sussex County Building",Shape="rect", Coords="347,399,394,422"},
                new MapCoordinateModel(){Title="Commercial Tent",Shape="rect", Coords="351,292,398,315"},
                new MapCoordinateModel(){Title="Livestock Barns",Shape="rect", Coords="634,399,681,422"},
                new MapCoordinateModel(){Title="Livestock Barns",Shape="rect", Coords="635,373,682,396"},
                new MapCoordinateModel(){Title="Livestock Barns",Shape="rect", Coords="635,348,682,371"},
                new MapCoordinateModel(){Title="Livestock Barns",Shape="rect", Coords="635,322,682,345"},
                new MapCoordinateModel(){Title="Livestock Barns",Shape="rect", Coords="635,298,682,321"},
                new MapCoordinateModel(){Title="Milking Parlor",Shape="rect", Coords="635,274,676,297"},
                new MapCoordinateModel(){Title="Snook Agricultural Museum &amp; Antique Engines",Shape="rect", Coords="541,269,585,290"},
                new MapCoordinateModel(){Title="Agriculture Honor Garden",Shape="rect", Coords="541,295,585,316"},
                new MapCoordinateModel(){Title="Livestock Pavilion",Shape="rect", Coords="540,338,584,356"},
                new MapCoordinateModel(){Title="Agriculture Division Food Booth",Shape="rect", Coords="541,363,567,399"},
                new MapCoordinateModel(){Title="Richards Exhibit Building",Shape="rect", Coords="445,350,500,391"},
                new MapCoordinateModel(){Title="Newton Rotary Food Booth",Shape="rect", Coords="452,260,492,281"},
                new MapCoordinateModel(){Title="4-H Shotwell Exhibit Building",Shape="rect", Coords="458,283,488,304"},
                new MapCoordinateModel(){Title="Board of Agriculture BBQ Pavilion",Shape="rect", Coords="473,306,503,327"},
                new MapCoordinateModel(){Title="Carnival",Shape="rect", Coords="215,334,239,352"},
                new MapCoordinateModel(){Title="ATM",Shape="rect", Coords="678,273,702,291"},
                new MapCoordinateModel(){Title="Bicycle Stunt Show",Shape="rect", Coords="437,392,461,410"},
                new MapCoordinateModel(){Title="Winegarden",Shape="rect", Coords="443,325,467,343"},
                new MapCoordinateModel(){Title="4-H Dairy Barn [ice cream]",Shape="rect", Coords="479,329,503,347"},
                new MapCoordinateModel(){Title="Horse Show Rings",Shape="rect", Coords="362,75,386,93"},
                new MapCoordinateModel(){Title="Horse Show Rings",Shape="rect", Coords="365,160,389,178"},
                new MapCoordinateModel(){Title="RESTROOMS",Shape="rect", Coords="229,209,253,227"},
                new MapCoordinateModel(){Title="ATM",Shape="rect", Coords="252,205,276,223"},
                new MapCoordinateModel(){Title="Branchville Rotary Food Booth",Shape="rect", Coords="307,200,331,218"},
                new MapCoordinateModel(){Title="INFORMATION BOOTHS",Shape="rect", Coords="401,211,425,229"},
                new MapCoordinateModel(){Title="SECURITY",Shape="rect", Coords="423,203,447,221"},
                new MapCoordinateModel(){Title="Horse Show Sponsor Pavilion",Shape="rect", Coords="400,83,424,101"},
                new MapCoordinateModel(){Title="Horse Show Office",Shape="rect", Coords="426,81,450,99"},
                new MapCoordinateModel(){Title="ATM",Shape="rect", Coords="447,97,471,115"},
                new MapCoordinateModel(){Title="Handicapped Seating for Horse Show",Shape="rect", Coords="448,142,472,160"},
                new MapCoordinateModel(){Title="Horse Show Rings",Shape="rect", Coords="541,88,565,106"},
                new MapCoordinateModel(){Title="Horse Show Rings",Shape="rect", Coords="540,140,564,158"},
                new MapCoordinateModel(){Title="Founders’ Park",Shape="rect", Coords="546,195,575,213"},
                new MapCoordinateModel(){Title="Boy Scout Food Booth",Shape="rect", Coords="585,202,608,223"},
                new MapCoordinateModel(){Title="Knights of Columbus Food Booth",Shape="rect", Coords="638,202,661,223"},
                new MapCoordinateModel(){Title="Beer Tent",Shape="rect", Coords="653,182,676,203"},
                new MapCoordinateModel(){Title="Miller Lite Outdoor Entertainment Area",Shape="rect", Coords="729,135,752,156"},
                new MapCoordinateModel(){Title="Sussex County Farmers’ Market",Shape="rect", Coords="724,329,756,351"},
                new MapCoordinateModel(){Title="20/33",Shape="rect", Coords="738,294,788,315"},
                new MapCoordinateModel(){Title="Summer Blossoms Garden", Shape="rect", Coords="792,292,824,315"},
                new MapCoordinateModel(){Title="Conservatory-Flower &amp; Garden Expo",Shape="rect", Coords="760,316,776,359"},
                new MapCoordinateModel(){Title="Conservatory Courtyard &amp; Tent",Shape="rect", Coords="777,314,793,357"},
                new MapCoordinateModel(){Title="Craft Beer Tent", Shape="rect", Coords="792,364,824,386"},
                new MapCoordinateModel(){Title="Greenhouse- Forage &amp; Vegetable Show",Shape="rect", Coords="746,358,796,393"},
                new MapCoordinateModel(){Title="INFORMATION BOOTHS",Shape="rect", Coords="126,227,141,241"},
                new MapCoordinateModel(){Title="Parking",Shape="rect", Coords="62,259,85,379"},
                new MapCoordinateModel(){Title="Carnival Wheel",Shape="rect", Coords="182,276,229,340"}
            };
        }

        public string MapUrl { get; set; }
        public List<MapCoordinateModel> MapCoordinates { get; set; }
    }
}