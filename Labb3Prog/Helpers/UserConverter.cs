using Labb3Prog.DataModels.Users;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Labb3Prog.Enums;

namespace Labb3Prog.Helpers
{
    class UserConverter : JsonConverter<User>
    {
        public override User ReadJson(JsonReader reader, Type objectType, User existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            if(jo["Type"] == null)
                throw new InvalidOperationException("Cannout determine user type.");
            User user;
            var type = (UserTypes)Enum.Parse(typeof(UserTypes), jo["Type"].ToString(),true);

            switch (type)
            {
                case UserTypes.Admin:
                    user = new Admin(jo["Name"].ToString(), jo["Password"].ToString()); 
                    break;
                case UserTypes.Customer:
                    user = new Customer(jo["Name"].ToString(), jo["Password"].ToString());
                    break;
                default:
                    throw new InvalidOperationException("Unknown user type.");
            }
            serializer.Populate(jo.CreateReader(), user);
            return user;
        }

        public override void WriteJson(JsonWriter writer, User value, JsonSerializer serializer)
        {
        //    throw new NotImplementedException();
        }
    }
}
