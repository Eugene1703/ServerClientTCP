using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [FirestoreData]
    internal class Userdata
    {
        [FirestoreProperty]
        public  string  Username { get; set; }
        [FirestoreProperty]
        public  string  Password { get; set; }
        [FirestoreProperty]
        public  string  Gender { get; set; }

    }
}
