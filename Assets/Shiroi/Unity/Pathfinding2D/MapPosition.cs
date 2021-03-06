using System;
using UnityEditor;
using UnityEngine;
using Vexe.Runtime.Types;

namespace Shiroi.Unity.Pathfinding2D {
    [Serializable]
    public class MapPosition {
        [SerializeField, Hide]
        private int x;

        [SerializeField, Hide]
        private int y;

        public MapPosition() {
        }

        public MapPosition(int x, int y) {
            this.x = x;
            this.y = y;
        }

        [Show]
        public int X {
            get { return x; }
            set { x = value; }
        }

        [Show]
        public int Y {
            get { return y; }
            set { y = value; }
        }

        public void Init(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public Vector2 ToWorldPosition(Vector2 vectorSize) {
            return new Vector2(vectorSize.x * X, vectorSize.y * Y);
        }

        public static implicit operator Vector2(MapPosition pos) {
            return new Vector2(pos.X, pos.Y);
        }

        public static implicit operator Vector3(MapPosition pos) {
            return new Vector2(pos.X, pos.Y);
        }

        public static implicit operator MapPosition(Vector2 pos) {
            return new MapPosition((int) pos.x, (int) pos.y);
        }

        public void SetPosition(Vector2 vector) {
            X = (int) vector.x;
            Y = (int) vector.y;
        }

        public bool IsWithin(MapPosition max, MapPosition min) {
            return X >= max.X && X <= min.X && Y >= min.Y && Y <= max.Y;
        }

        public bool IsWithin(Vector2 max, Vector2 min) {
            return X >= max.x && X <= min.x && Y >= min.y && Y <= max.y;
        }

        public float Distance(MapPosition to) {
            return Vector2.Distance(this, to);
        }

        public void DrawGizmos() {
            Gizmos.DrawCube((Vector2) this, Vector2.one);
        }
    }
}