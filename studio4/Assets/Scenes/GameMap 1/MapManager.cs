using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GameMap
{
    public class MapManager : MonoBehaviour
    {
        public MapConfig config;
        public MapView mapView;
        public Map CurrentMap { get; private set; }

        private void Start()
        {
            if (PlayerPrefs.HasKey("MAP"))
            {
                string mapJson = PlayerPrefs.GetString("Map");
                Map map = JsonConvert.DeserializeObject<Map>(mapJson);

                if(map.path.Any(p => p.Equals(map.GetPlayerBattle().point)))
                {
                    // if a player has reached the end of the map then generate a new map
                    GenerateNewMap();
                }
                else
                {
                    CurrentMap = map;
                    // player has not reached the end yet load the current map
                    mapView.ShowMap(map);
                }
            }
            else
            {
                GenerateNewMap();
            }
        }

        public void GenerateNewMap()
        {
            MapConfig map = MapGenerator.GetMap(config);
            CurrentMap = map;
            Debug.Log(map.ToJson());
            mapView.ShowMap(map);
        }

        public void SaveMap()
        {
            if (CurrentMap == null) return;

            string json = JsonConvert.SerializeObject(CurrentMap);
            PlayerPrefs.SetString("Map", json);
            PlayerPrefs.Save();
        }

        private void OnApplicationQuit()
        {
            SaveMap();
        }
    }
}