namespace MarkEmbling.PostcodesIO {
    public class QueryResult<TQuery, TResult> where TResult : class {
        public TQuery Query { get; set; }
        public TResult Result { get; set; }
    }




    public class PostcodeBulkLookupResult {
        /*
{
    "status": 200,
    "result": [
        {
            "query": "NE30 1DP",
            "result": {
                "postcode": "NE30 1DP",
                "quality": 1,
                "eastings": 435982,
                "northings": 568683,
                "country": "England",
                "nhs_ha": "North East",
                "longitude": -1.43889223951028,
                "latitude": 55.0114112900573,
                "parliamentary_constituency": "Tynemouth",
                "european_electoral_region": "North East",
                "primary_care_trust": "North Tyneside",
                "region": "North East",
                "lsoa": "North Tyneside 016C",
                "msoa": "North Tyneside 016",
                "nuts": null,
                "incode": "1DP",
                "outcode": "NE30",
                "admin_district": "North Tyneside",
                "parish": null,
                "admin_county": null,
                "admin_ward": "Tynemouth",
                "ccg": "NHS North Tyneside",
                "codes": {
                    "admin_district": "E08000022",
                    "admin_county": "E99999999",
                    "admin_ward": "E05001130",
                    "parish": "E43000176",
                    "ccg": "E38000127"
                }
            }
        },
        {
            "query": "M32 0JG",
            "result": {
                "postcode": "M32 0JG",
                "quality": 1,
                "eastings": 379988,
                "northings": 395476,
                "country": "England",
                "nhs_ha": "North West",
                "longitude": -2.30283674284007,
                "latitude": 53.4556572899372,
                "parliamentary_constituency": "Stretford and Urmston",
                "european_electoral_region": "North West",
                "primary_care_trust": "Trafford",
                "region": "North West",
                "lsoa": "Trafford 003C",
                "msoa": "Trafford 003",
                "nuts": null,
                "incode": "0JG",
                "outcode": "M32",
                "admin_district": "Trafford",
                "parish": null,
                "admin_county": null,
                "admin_ward": "Gorse Hill",
                "ccg": "NHS Trafford",
                "codes": {
                    "admin_district": "E08000009",
                    "admin_county": "E99999999",
                    "admin_ward": "E05000829",
                    "parish": "E43000163",
                    "ccg": "E38000187"
                }
            }
        },
        {
            "query": "OX49 5NU",
            "result": {
                "postcode": "OX49 5NU",
                "quality": 1,
                "eastings": 464435,
                "northings": 195686,
                "country": "England",
                "nhs_ha": "South Central",
                "longitude": -1.06993881320412,
                "latitude": 51.6562791294687,
                "parliamentary_constituency": "Henley",
                "european_electoral_region": "South East",
                "primary_care_trust": "Oxfordshire",
                "region": "South East",
                "lsoa": "South Oxfordshire 011B",
                "msoa": "South Oxfordshire 011",
                "nuts": null,
                "incode": "5NU",
                "outcode": "OX49",
                "admin_district": "South Oxfordshire",
                "parish": "Brightwell Baldwin",
                "admin_county": "Oxfordshire",
                "admin_ward": "Benson",
                "ccg": "NHS Oxfordshire",
                "codes": {
                    "admin_district": "E07000179",
                    "admin_county": "E10000025",
                    "admin_ward": "E05006570",
                    "parish": "E04008109",
                    "ccg": "E38000136"
                }
            }
        }
    ]
}
         */
    }
}