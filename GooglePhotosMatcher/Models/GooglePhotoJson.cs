namespace GooglePhotosMatcher.Models;

// "{\n  \"title\": \"IMG_20140607_211105.jpg\",\n  \"description\": \"\",\n  \"imageViews\": \"54\",\n  \"creationTime\": {\n    \"timestamp\": \"1402473545\",\n    \"formatted\": \"11 Jun 2014, 07:59:05 UTC\"\n  },\n  \"photoTakenTime\": {\n    \"timestamp\": \"1402168265\",\n    \"formatted\": \"7 Jun 2014, 19:11:05 UTC\"\n  },\n  \"geoData\": {\n    \"latitude\": 54.6009483,\n    \"longitude\": 18.5212765,\n    \"altitude\": 52.4,\n    \"latitudeSpan\": 0.0,\n    \"longitudeSpan\": 0.0\n  },\n  \"geoDataExif\": {\n    \"latitude\": 54.6009483,\n    \"longitude\": 18.5212765,\n    \"altitude\": 52.4,\n    \"latitudeSpan\": 0.0,\n    \"longitudeSpan\": 0.0\n  },\n  \"url\": \"https://photos.google.com/photo/AF1QipPbmmyEeh7oT-Srfej_k8ckQ-vO3oO_CpNec7OM\",\n  \"googlePhotosOrigin\": {\n    \"mobileUpload\": {\n      \"deviceType\": \"ANDROID_PHONE\"\n    }\n  }\n}"

internal class GooglePhotoJson
{
    internal class GoogleDate
    {
        public string timestamp { get; set; }

        public string formatted { get; set; }
    }

    public string title { get; set; }

    public GoogleDate creationTime { get; set; }

    public GoogleDate photoTakenTime { get; set; }
}