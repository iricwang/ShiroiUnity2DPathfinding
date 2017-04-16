﻿using Shiroi.Unity.Pathfinding2D.Util;
using UnityEditor;
using UnityEngine;
using Vexe.Runtime.Types;

namespace Shiroi.Unity.Pathfinding2D {
    public class TileMap : BaseBehaviour {
        public const byte DefaultColorAlpha = 0x9B; //155

        public Color MaxColor = GetColor("E91E63");
        public Color MinColor = GetColor("00BCD4");
        public Color BorderLineColor = Color.green;
        private MapPosition mapMinPos = new MapPosition(0, 0);
        private MapPosition mapMaxPos = new MapPosition(1, 0);


        private static Color GetColor(string color) {
            return ColorUtil.FromHex(color, DefaultColorAlpha);
        }

        [Show]
        public Vector2 Center {
            get {
                return new Vector2(XCenter, YCenter);
            }
        }

        [Show]
        public Vector2 Size {
            get {
                return new Vector2(XSize, YSize);
            }
        }

        public float XCenter {
            get {
                return (float) (mapMaxPos.X + mapMinPos.X) / 2;
            }
        }


        public float YCenter {
            get {
                return (float) (mapMaxPos.Y + mapMinPos.Y) / 2;
            }
        }

        public int XSize {
            get {
                AdjustXy();
                return MapMaxPos.X - MapMinPos.X;
            }
        }

        public int YSize {
            get {
                AdjustXy();
                return MapMaxPos.Y - MapMinPos.Y;
            }
        }

        [Show]
        public MapPosition MapMinPos {
            get {
                return mapMinPos;
            }
            set {
                mapMinPos = value;
                AdjustXy();
            }
        }


        [Show]
        public MapPosition MapMaxPos {
            get {
                return mapMaxPos;
            }
            set {
                mapMaxPos = value;
                AdjustXy();
            }
        }

        public Vector2 NodeSize = Vector2.one;

        private Node[,] nodeMap;

        public void AdjustXy() {
            var minX = Mathf.Min(MapMaxPos.X, MapMinPos.X);
            var minY = Mathf.Min(MapMaxPos.Y, MapMinPos.Y);
            var maxX = Mathf.Max(MapMaxPos.X, MapMinPos.X);
            var maxY = Mathf.Max(MapMaxPos.Y, MapMinPos.Y);
            mapMinPos.X = minX;
            mapMinPos.Y = minY;
            mapMaxPos.X = maxX;
            mapMaxPos.Y = maxY;
        }

        public bool IsOutOfBounds(MapPosition position) {
            AdjustXy();
            return position.IsWithin(mapMaxPos, mapMinPos);
        }

        [Show]
        public void RecreateNodeMap() {
            nodeMap = new Node[XSize, YSize];
        }

        public Node GetNode(int x, int y) {
            if (nodeMap == null) {
                Debug.LogError("The node map isn't initialized!");
                return null;
            }
            return null;
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = MinColor;
            Gizmos.DrawCube(mapMinPos.ToWorldPosition(NodeSize), NodeSize);
            Gizmos.color = MaxColor;
            Gizmos.DrawCube(mapMaxPos.ToWorldPosition(NodeSize), NodeSize);
            Gizmos.color = BorderLineColor;
            Gizmos.DrawWireCube(Center, Size);
        }
    }
}