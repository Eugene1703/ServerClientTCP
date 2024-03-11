using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
namespace Server
{
    internal static class FirestoreHelper
    {
        static string config = @"
       {
  ""type"": ""service_account"",
  ""project_id"": ""azir-auth"",
  ""private_key_id"": ""06f686a9221ee9a2d205b251dc0bf29073595d3b"",
  ""private_key"": ""-----BEGIN PRIVATE KEY-----\nMIIEvwIBADANBgkqhkiG9w0BAQEFAASCBKkwggSlAgEAAoIBAQDkOnjWzJD4Ozhm\nfoRKbkjQ3Mjx1Fe8y2dSolyqpVA2IYonf8/FF4M1AWQsEwN3rL1rgCjXHDhHAl/f\nXAbOd5m0InjaUDF+UFF0cSZFj8MlNkiCl2UJUw2yJVS9a+NJFI72dCBBDVM8fHUf\ngIuHj6zmV2+kYwQZxhjTERdeh8O20KX0yj4dTq8IEm4HvEz/BajKvvXIdfc1KTkG\nJF43wCkozGGFoKQIviVyepz90wbG4NwCPM7g4D7PljH9uBB9FvGj6jHOwbP3CEKx\n7FHUWEHR1X6kkGZfFvbZvrk8IuyVvQbf8mFMrrMGrTjQF+OmsimGtnirhgug6w4C\n41rZiAMdAgMBAAECggEAGSmPMBNuukeBMN+rAy/EjG6DIxJnt/NvCU/ISrIeNVGe\nUzuGszKj7yCfpDJWdAfR/HutRQYh+EY9Prl9MJ9EDXWtgFhOFKCTUnS1JP80IzKo\nbOl3JL+I54axII84CFMhbe+grLtk3WrVWOtJtuDyzZR/RQ77oUHTc9XTD0OFou6J\nxJQOnmpU8aLlGOUKuA6n2FbOteOrTNHFkh+rnwQUiOnCvBapSHw/prsMtUYhHWLK\nONI9ulFv44In1OwLy8KbB+74TU7xMDSk4n2oqTXRv3JX4MBIW5YehbiTVmsvC90X\nFtpnhVfRxz/grbJAgT0vBmLqsWZlpvYaibZMiBxpewKBgQD31v9vn+iKnZD4nJ55\nj7tMUuVXbN5EteOMxo3ptFfblC0cLugr5zAY2TXICG1aj4Bb6LPzSzhj77yGBJaf\nMupSYKlnlMES5DkUx7nXzZTCg33tMgf6Yxb1QZPMdIXp1+b/AHHyVIjNpCrttHzn\n8kAVn4nZuLJvIBySAwGkPKNLfwKBgQDrviwyZ1PrIHHg/dhhAoHCdC161QiNlFUx\n8o3fVBDTC77MYsU/ENSsmgYEOoJgAXSsivcdSZ3tXu+3BkwXti+2QWSZ9gDlISEF\nBhQ/2tlJzBSF4Eohw1SXeCKq6loMklvH7HyurnoS1tIDe7rK1FN08YonSjq6jYjd\nQvls1TqvYwKBgQDD7rnEIpQxub2ZfiAfFE1LWqGrCwtxIKbDbVlV8Fgg5LUA+ehc\nhQnWKypEeL0TAi3E+2QALHBpOWc+QDRfqV3sQDTNCaHV8I1b60X14LYBTPjp40rB\nXUMSI4TX9yrDIwegg1aR8NiyUl4JzE2PBUo6jgTqFgUiqy1LFYtfrvyduwKBgQCl\neT2ou6LKHmIRHd2uaoYOqmHqIHL6fi42+xzesDGHEm2BJatykyvVY9/pQ/CT1zHA\nk2LV6gCyx7rX69GpqzZeZhRphaYnL1PwJM497C4tgNS/DCq8/FD6aVdcRVekD2/h\nK3xxWQNTCfssesMo8FUMdycUYSRMImD5frN500p4JQKBgQCmtQlyZhUPnBBvooul\nwVyfU8w6vfRU7S7kf3UgYTaT3hUGKzQGoeWz0XpmC4FEt7i8g6VoSyXyH89SUTRk\nkqep93dFm7T8Yd2y3+MecCSJAmTsLKMVHlm1SOZzQQKMy4pazObkrh2bpZpzDvgV\nqV4f6LqVxUM3eVw2PDMqul57BA==\n-----END PRIVATE KEY-----\n"",
  ""client_email"": ""firebase-adminsdk-99t7e@azir-auth.iam.gserviceaccount.com"",
  ""client_id"": ""104728811930395232912"",
  ""auth_uri"": ""https://accounts.google.com/o/oauth2/auth"",
  ""token_uri"": ""https://oauth2.googleapis.com/token"",
  ""auth_provider_x509_cert_url"": ""https://www.googleapis.com/oauth2/v1/certs"",
  ""client_x509_cert_url"": ""https://www.googleapis.com/robot/v1/metadata/x509/firebase-adminsdk-99t7e%40azir-auth.iam.gserviceaccount.com"",
  ""universe_domain"": ""googleapis.com""
}";
        public static FirestoreDb Database { get; private set; }

        static string filepath = "";


        public static void SetEnviromentVariable()
        {
            filepath = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(Path.GetRandomFileName())) + ".json";
            File.WriteAllText(filepath, config);
            File.SetAttributes(filepath, FileAttributes.Hidden);
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filepath);
            Database = FirestoreDb.Create("azir-auth");
            File.Delete(filepath);
        }

    }


    


}
