using System;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Configuration.Provider;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace App_Code.HostelMapping
{

    #region UsersMapping Object

    public class MeterMapping
    {
        #region Constructer

        public MeterMapping()
        {
            //this.UserId = Guid.NewGuid();
        }

        #endregion

        #region Fields & Properties

        private List<int> meterId;
        public List<int> MeterId
        {
            get { return meterId; }
            set { meterId = value; }
        }

        private string groupId;
        public string GroupId
        {
            get { return groupId; }
            set { groupId = value; }
        }

        private string building;
        public string Building
        {
            get { return building; }
            set { building = value; }
        }

        #endregion
    }

    public class OccupantMapping
    {
        #region Constructer

        public OccupantMapping()
        {
            //this.UserId = Guid.NewGuid();
        }

        #endregion

        #region Fields & Properties

        private List<string> roomNos;
        public List<string> RoomNos
        {
            get { return roomNos; }
            set { roomNos = value; }
        }

        private string building;
        public string Building
        {
            get { return building; }
            set { building = value; }
        }

        private List<string> occupantNames;
        public List<string> OccupantNames
        {
            get { return occupantNames; }
            set { occupantNames = value; }
        }

        private string groupId;
        public string GroupId
        {
            get { return groupId; }
            set { groupId = value; }
        }

        #endregion
    }


    public class GroupMapping
    {
        #region Constructer

        public GroupMapping()
        {
            //this.UserId = Guid.NewGuid();
        }
        #endregion

        #region Fields & Properties

        private string groupId = "";
        public string GroupId
        {
            get { return groupId; }
            set { groupId = value; }
        }
        private string building = "";
        public string Building
        {
            get { return building; }
            set { building = value; }
        }

        private string groupName = "";
        public string GroupName
        {
            get { return groupName; }
            set { groupName = value; }
        }

        private int occupantCount = 1;
        public int OccupantCount
        {
            get { return occupantCount; }
            set { occupantCount = value; }
        }

        private string roomNos = "";
        public string RoomNos
        {
            get { return roomNos; }
            set { roomNos = value; }
        }

        private MeterMapping meters;
        public MeterMapping Meters
        {
            get { return meters; }
            set { meters = value; }
        }

        private OccupantMapping occupants;
        public OccupantMapping Occupants
        {
            get { return occupants; }
            set { occupants = value; }
        }

        #endregion
    }

    #endregion

    public static class Group_Mapping
    {
        #region Feilds

        private static string connString = ConfigurationManager.ConnectionStrings["HostelAppConnectionString"].ConnectionString;

        private static DbProviderFactory provider = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["HostelAppConnectionString"].ProviderName);
        private static string parmPrefix = "@";

        #endregion

        #region Methods

        public static GroupMapping MapGroup(string GroupId, string building)
        {
            GroupMapping userDetail = new GroupMapping();

            try
            {
                using (DbConnection conn = provider.CreateConnection())
                {
                    conn.ConnectionString = connString;

                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        string sqlQuery = "SELECT group_id,building,rooms,student_number,group_name" +
                                         " FROM group_address_mapping WHERE group_id = @GrpId AND building=@build";

                        if (parmPrefix != "@")
                        {
                            sqlQuery = sqlQuery.Replace("@", parmPrefix);
                        }
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;

                        DbParameter dpID = provider.CreateParameter();
                        dpID.ParameterName = parmPrefix + "GrpId";
                        dpID.Value = GroupId;
                        cmd.Parameters.Add(dpID);

                        DbParameter dpBuild = provider.CreateParameter();
                        dpBuild.ParameterName = parmPrefix + "build";
                        dpBuild.Value = building;
                        cmd.Parameters.Add(dpBuild);

                        conn.Open();

                        using (DbDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                    userDetail = new GroupMapping();
                                    rdr.Read();
                                    if (!rdr.IsDBNull(0))
                                    {
                                        userDetail.GroupId =rdr.GetString(0);
                                    }
                                    if (!rdr.IsDBNull(1))
                                    {
                                        userDetail.Building = rdr.GetString(1);
                                    }
                                    if (!rdr.IsDBNull(2))
                                    {
                                        userDetail.RoomNos = rdr.GetString(2);
                                    }
                                    if (!rdr.IsDBNull(3))
                                    {
                                        userDetail.OccupantCount = rdr.GetInt32(3);
                                    }
                                    if (!rdr.IsDBNull(4))
                                    {
                                        userDetail.GroupName = rdr.GetString(4);
                                    }
                                    userDetail.Meters = MapMeter(GroupId, building);
                                    userDetail.Occupants = MapOccupants(GroupId,building);
                            }
                            else
                            {
                                return null;
                            }
                        }

                    }
                    conn.Close();
                }
            }
            catch (Exception exp)
            {
                return null;
            }
            return userDetail;

        }

        public static MeterMapping MapMeter(string GroupId, string Building)
        {
            MeterMapping mtrMap = new MeterMapping();
            try
            {
                using (DbConnection conn = provider.CreateConnection())
                {
                    conn.ConnectionString = connString;

                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        string sqlQuery = "SELECT group_id,meter_id,building" +
                                         " FROM group_meter_mapping WHERE group_id = @GrpId AND building=@build";

                        if (parmPrefix != "@")
                        {
                            sqlQuery = sqlQuery.Replace("@", parmPrefix);
                        }
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;

                        DbParameter dpID = provider.CreateParameter();
                        dpID.ParameterName = parmPrefix + "GrpId";
                        dpID.Value = GroupId;
                        cmd.Parameters.Add(dpID);

                        DbParameter dpBuild = provider.CreateParameter();
                        dpBuild.ParameterName = parmPrefix + "build";
                        dpBuild.Value = Building;
                        cmd.Parameters.Add(dpBuild);

                        conn.Open();

                        using (DbDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                mtrMap.MeterId = new List<int>();
                                while (rdr.Read())
                                {
                                    if (!rdr.IsDBNull(0))
                                    {
                                        mtrMap.GroupId = rdr.GetString(0);
                                    }
                                    if (!rdr.IsDBNull(2))
                                    {
                                        mtrMap.Building = rdr.GetString(2);
                                    }
                                    if (!rdr.IsDBNull(1))
                                    {
                                        mtrMap.MeterId.Add(rdr.GetInt32(1));
                                    }
                                }                                
                            }                           
                        }
                        conn.Close();
                        return mtrMap;
                    } 
                } 
            }
            catch (Exception exp)
            {
                return mtrMap;
            }

        }

        public static OccupantMapping MapOccupants(string GroupId, string Building)
        {
            OccupantMapping occMap = new OccupantMapping();
            try
            {
                using (DbConnection conn = provider.CreateConnection())
                {
                    conn.ConnectionString = connString;

                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        string sqlQuery = "SELECT group_id,room_no,occupant_names,building" +
                                         " FROM room_occupant_names WHERE group_id = @GrpId AND building=@build";

                        if (parmPrefix != "@")
                        {
                            sqlQuery = sqlQuery.Replace("@", parmPrefix);
                        }
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;

                        DbParameter dpID = provider.CreateParameter();
                        dpID.ParameterName = parmPrefix + "GrpId";
                        dpID.Value = GroupId;
                        cmd.Parameters.Add(dpID);

                        DbParameter dpBuild = provider.CreateParameter();
                        dpBuild.ParameterName = parmPrefix + "build";
                        dpBuild.Value = Building;
                        cmd.Parameters.Add(dpBuild);

                        conn.Open();

                        using (DbDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                occMap.RoomNos = new List<string>();
                                occMap.OccupantNames = new List<string>();
                                while (rdr.Read())
                                {
                                    if (!rdr.IsDBNull(0))
                                    {
                                        occMap.GroupId= rdr.GetString(0);
                                    }
                                    if (!rdr.IsDBNull(1))
                                    {
                                        occMap.RoomNos.Add(rdr.GetString(1));
                                    }
                                    if (!rdr.IsDBNull(2))
                                    {
                                        occMap.OccupantNames.Add(rdr.GetString(2));
                                    }
                                    if (!rdr.IsDBNull(3))
                                    {
                                        occMap.Building = rdr.GetString(3);
                                    }
                                }                               
                            }
                        } 
                        conn.Close();
                        return occMap;                      
                    }
                }
            }
            catch (Exception exp)
            {
                return occMap;
            }

        }

        public static List<GroupMapping> ListHostelGroups(string building)
        {
            List<GroupMapping> allGroups = new List<GroupMapping>();

            try
            {
                using (DbConnection conn = provider.CreateConnection())
                {
                    conn.ConnectionString = connString;

                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        string sqlQuery = "SELECT group_id,building,rooms,student_number,group_name" +
                                         " FROM group_address_mapping WHERE building=@build";

                        if (parmPrefix != "@")
                        {
                            sqlQuery = sqlQuery.Replace("@", parmPrefix);
                        }
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;

                        DbParameter dpID = provider.CreateParameter();
                        dpID.ParameterName = parmPrefix + "build";
                        dpID.Value = building;
                        cmd.Parameters.Add(dpID);


                        conn.Open();

                        using (DbDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {                                
                                while (rdr.Read())
                                {
                                    GroupMapping grpMap = new GroupMapping();
                                    if (!rdr.IsDBNull(0))
                                    {
                                        grpMap.GroupId = rdr.GetString(0);
                                        grpMap.Meters = MapMeter(grpMap.GroupId, building);
                                        grpMap.Occupants = MapOccupants(grpMap.GroupId, building);
                                    }
                                    if (!rdr.IsDBNull(1))
                                    {
                                        grpMap.Building = rdr.GetString(1);
                                    }
                                    if (!rdr.IsDBNull(2))
                                    {
                                        grpMap.RoomNos = rdr.GetString(2);
                                    }
                                    if (!rdr.IsDBNull(3))
                                    {
                                        grpMap.OccupantCount = rdr.GetInt32(3);
                                    }
                                    if (!rdr.IsDBNull(4))
                                    {
                                        grpMap.GroupName = rdr.GetString(4);
                                    }
                                    allGroups.Add(grpMap);
                                }
                            }
                            else
                            {
                                return null;
                            }
                        }

                    }
                    conn.Close();
                }
            }
            catch (Exception exp)
            {
                return null;
            }
            return allGroups;

        }

        public static List<GroupMapping> ListAllGroups()
        {
            List<GroupMapping> allGroups = new List<GroupMapping>();

            try
            {
                using (DbConnection conn = provider.CreateConnection())
                {
                    conn.ConnectionString = connString;

                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        string sqlQuery = "SELECT group_id,building,rooms,student_number,group_name" +
                                         " FROM group_address_mapping";

                        if (parmPrefix != "@")
                        {
                            sqlQuery = sqlQuery.Replace("@", parmPrefix);
                        }
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;

                      
                        conn.Open();

                        using (DbDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    GroupMapping grpMap = new GroupMapping();
                                    if (!rdr.IsDBNull(0))
                                    {
                                        grpMap.GroupId = rdr.GetString(0);
                                    }
                                    if (!rdr.IsDBNull(1))
                                    {
                                        grpMap.Building = rdr.GetString(1);
                                    }
                                    if (grpMap.Building != null && grpMap.GroupId != null)
                                    {
                                        grpMap.Meters = MapMeter(grpMap.GroupId, grpMap.Building);
                                        grpMap.Occupants = MapOccupants(grpMap.GroupId, grpMap.Building);
                                    }
                                    if (!rdr.IsDBNull(2))
                                    {
                                        grpMap.RoomNos = rdr.GetString(2);
                                    }
                                    if (!rdr.IsDBNull(3))
                                    {
                                        grpMap.OccupantCount = rdr.GetInt32(3);
                                    }
                                    if (!rdr.IsDBNull(4))
                                    {
                                        grpMap.GroupName = rdr.GetString(4);
                                    }
                                    allGroups.Add(grpMap);
                                }
                            }
                            else
                            {
                                return null;
                            }
                        }

                    }
                    conn.Close();
                }
            }
            catch (Exception exp)
            {
                return null;
            }
            return allGroups;

        }

        public static MeterMapping ListAllMeters()
        {
            MeterMapping allMeters = new MeterMapping();

            try
            {
                using (DbConnection conn = provider.CreateConnection())
                {
                    conn.ConnectionString = connString;

                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        string sqlQuery = "SELECT group_id, meter_id, building" +
                                         " FROM group_meter_mapping";

                        if (parmPrefix != "@")
                        {
                            sqlQuery = sqlQuery.Replace("@", parmPrefix);
                        }
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;

                        conn.Open();

                        using (DbDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                allMeters.MeterId = new List<int>();
                                while (rdr.Read())
                                {
                                    if (!rdr.IsDBNull(0))
                                    {
                                        allMeters.GroupId = rdr.GetString(0);
                                    }
                                    if (!rdr.IsDBNull(1))
                                    {
                                        allMeters.MeterId.Add(rdr.GetInt32(1));
                                    }
                                    if (!rdr.IsDBNull(2))
                                    {
                                        allMeters.Building = rdr.GetString(2);
                                    }                                    
                                }
                            }
                            else
                            {
                                return null;
                            }
                        }

                    }
                    conn.Close();
                }
            }
            catch (Exception exp)
            {
                return null;
            }
            return allMeters;

        }

        public static bool InsertGroupMap(GroupMapping insertMap)
        {
            try
            {
                using (DbConnection conn = provider.CreateConnection())
                {
                    conn.ConnectionString = connString;
                    conn.Open();

                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        string sqlQuery;
                        sqlQuery = "INSERT INTO group_address_mapping" +
                               "(group_id,building,rooms,student_number,group_name) " +
                               "VALUES(@grpId,@build,@rums,@occupants,@grpName)";

                        if (parmPrefix != "@")
                        {
                            sqlQuery = sqlQuery.Replace("@", parmPrefix);
                        }
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;

                        DbParameter sNewId = provider.CreateParameter();
                        sNewId.ParameterName = parmPrefix + "grpId";
                        sNewId.Value = insertMap.GroupId;
                        cmd.Parameters.Add(sNewId);

                        DbParameter sBuild = provider.CreateParameter();
                        sBuild.ParameterName = parmPrefix + "build";
                        sBuild.Value = insertMap.Building;
                        cmd.Parameters.Add(sBuild);

                        DbParameter sRooms = provider.CreateParameter();
                        sRooms.ParameterName = parmPrefix + "rums";
                        sRooms.Value = insertMap.RoomNos;
                        cmd.Parameters.Add(sRooms);

                        DbParameter dOccupants = provider.CreateParameter();
                        dOccupants.ParameterName = parmPrefix + "occupants";
                        dOccupants.Value = insertMap.OccupantCount;
                        cmd.Parameters.Add(dOccupants);

                        DbParameter dGroupName = provider.CreateParameter();
                        dGroupName.ParameterName = parmPrefix + "grpName";
                        dGroupName.Value = insertMap.GroupName;
                        cmd.Parameters.Add(dGroupName);

                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                    conn.Close();
                }
                return true;
            }
            catch (Exception exp)
            {
                return false;
            }

        }

        public static bool InsertMeterMap(MeterMapping insertMap)
        {
            try
            {
                if (insertMap.MeterId != null)
                {
                    using (DbConnection conn = provider.CreateConnection())
                    {
                        conn.ConnectionString = connString;
                        conn.Open();

                        for (int i = 0; i < insertMap.MeterId.Count; i++)
                        {
                            using (DbCommand cmd = conn.CreateCommand())
                            {
                                string sqlQuery;
                                sqlQuery = "INSERT INTO group_meter_mapping" +
                                       "(group_id,meter_id,building) " +
                                       "VALUES(@grpId,@metId,@build)";

                                if (parmPrefix != "@")
                                {
                                    sqlQuery = sqlQuery.Replace("@", parmPrefix);
                                }
                                cmd.CommandText = sqlQuery;
                                cmd.CommandType = CommandType.Text;

                                DbParameter sNewId = provider.CreateParameter();
                                sNewId.ParameterName = parmPrefix + "grpId";
                                sNewId.Value = insertMap.GroupId;
                                cmd.Parameters.Add(sNewId);

                                DbParameter sBuild = provider.CreateParameter();
                                sBuild.ParameterName = parmPrefix + "build";
                                sBuild.Value = insertMap.Building;
                                cmd.Parameters.Add(sBuild);

                                DbParameter sMeterId = provider.CreateParameter();
                                sMeterId.ParameterName = parmPrefix + "metId";
                                sMeterId.Value = insertMap.MeterId[i]; ;
                                cmd.Parameters.Add(sMeterId);

                                cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();
                            }
                        }
                        conn.Close();
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exp)
            {
                return false;
            }

        }

        public static bool InsertOccupantMap(OccupantMapping insertMap)
        {
            try
            {
                if (insertMap.RoomNos != null)
                {
                    using (DbConnection conn = provider.CreateConnection())
                    {
                        conn.ConnectionString = connString;
                        conn.Open();

                        for (int i = 0; i < insertMap.RoomNos.Count; i++)
                        {

                            using (DbCommand cmd = conn.CreateCommand())
                            {
                                string sqlQuery;
                                sqlQuery = "INSERT INTO room_occupant_names" +
                                       "(room_no,building,occupant_names,group_id) " +
                                       "VALUES(@room,@build,@names,@grpId)";

                                if (parmPrefix != "@")
                                {
                                    sqlQuery = sqlQuery.Replace("@", parmPrefix);
                                }
                                cmd.CommandText = sqlQuery;
                                cmd.CommandType = CommandType.Text;

                                DbParameter sNewId = provider.CreateParameter();
                                sNewId.ParameterName = parmPrefix + "room";
                                sNewId.Value = insertMap.RoomNos[i];
                                cmd.Parameters.Add(sNewId);

                                DbParameter sBuild = provider.CreateParameter();
                                sBuild.ParameterName = parmPrefix + "build";
                                sBuild.Value = insertMap.Building;
                                cmd.Parameters.Add(sBuild);

                                DbParameter sNames = provider.CreateParameter();
                                sNames.ParameterName = parmPrefix + "names";
                                sNames.Value = insertMap.OccupantNames[i];
                                cmd.Parameters.Add(sNames);

                                DbParameter sGrpId = provider.CreateParameter();
                                sGrpId.ParameterName = parmPrefix + "grpId";
                                sGrpId.Value = insertMap.GroupId;
                                cmd.Parameters.Add(sGrpId);

                                cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();
                            }
                        }
                        conn.Close();
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }            
            catch (Exception exp)
            {
                return false;
            }

        }

        public static bool UpdateOccupantMap(OccupantMapping updateMap)
        {
            try
            {
                if (updateMap.RoomNos != null)
                {
                    using (DbConnection conn = provider.CreateConnection())
                    {
                        conn.ConnectionString = connString;
                        conn.Open();

                        for (int i = 0; i < updateMap.RoomNos.Count; i++)
                        {

                            using (DbCommand cmd = conn.CreateCommand())
                            {
                                string sqlQuery;
                                sqlQuery = "UPDATE room_occupant_names " +
                                       "SET occupant_names=@names WHERE building=@build AND room_no = @room";

                                if (parmPrefix != "@")
                                {
                                    sqlQuery = sqlQuery.Replace("@", parmPrefix);
                                }
                                cmd.CommandText = sqlQuery;
                                cmd.CommandType = CommandType.Text;

                                DbParameter sNewId = provider.CreateParameter();
                                sNewId.ParameterName = parmPrefix + "room";
                                sNewId.Value = updateMap.RoomNos[i];
                                cmd.Parameters.Add(sNewId);

                                DbParameter sBuild = provider.CreateParameter();
                                sBuild.ParameterName = parmPrefix + "build";
                                sBuild.Value = updateMap.Building;
                                cmd.Parameters.Add(sBuild);

                                DbParameter sNames = provider.CreateParameter();
                                sNames.ParameterName = parmPrefix + "names";
                                sNames.Value = updateMap.OccupantNames;
                                cmd.Parameters.Add(sNames);

                                DbParameter sGrpId = provider.CreateParameter();
                                sGrpId.ParameterName = parmPrefix + "grpId";
                                sGrpId.Value = updateMap.GroupId;
                                cmd.Parameters.Add(sGrpId);

                                cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();
                            }
                        }
                        conn.Close();
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exp)
            {
                return false;
            }

        }
        
      /*  public static bool UpdateMap(GroupMapping insertMap)
        {
            try
            {
                using (DbConnection conn = provider.CreateConnection())
                {
                    conn.ConnectionString = connString;
                    conn.Open();

                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        string sqlQuery;
                        sqlQuery = "Update meter_map " +
                               "SET MeterNo = @meterID, MeterType=@metType WHERE UserID = @userID ";
                             

                        if (parmPrefix != "@")
                        {
                            sqlQuery = sqlQuery.Replace("@", parmPrefix);
                        }
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;

                        DbParameter sNewId = provider.CreateParameter();
                        sNewId.ParameterName = parmPrefix + "userID";
                        sNewId.Value = insertMap.UserId;
                        cmd.Parameters.Add(sNewId);

                        //DbParameter sDeviceId = provider.CreateParameter();
                        //sDeviceId.ParameterName = parmPrefix + "apartment";
                        //sDeviceId.Value = insertMap.Apartment;
                        //cmd.Parameters.Add(sDeviceId);


                        DbParameter sMeterID = provider.CreateParameter();
                        sMeterID.ParameterName = parmPrefix + "meterID";
                        sMeterID.Value = insertMap.MeterId;
                        cmd.Parameters.Add(sMeterID);

                        //DbParameter dFloor = provider.CreateParameter();
                        //dFloor.ParameterName = parmPrefix + "floor";
                        //dFloor.Value = insertMap.Floor;
                        //cmd.Parameters.Add(dFloor);

                        //DbParameter dBuilding = provider.CreateParameter();
                        //dBuilding.ParameterName = parmPrefix + "building";
                        //dBuilding.Value = insertMap.Building;
                        //cmd.Parameters.Add(dBuilding);

                        DbParameter dMetType = provider.CreateParameter();
                        dMetType.ParameterName = parmPrefix + "metType";
                        dMetType.Value = insertMap.GroupId;
                        cmd.Parameters.Add(dMetType);

                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                    conn.Close();
                }
                return true;
            }
            catch (Exception exp)
            {
                return false;
            }

        }
        */
  
        #endregion

    }
}