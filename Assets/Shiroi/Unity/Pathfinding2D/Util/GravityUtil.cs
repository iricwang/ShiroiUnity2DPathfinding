﻿using System.Collections.Generic;
using System.Linq;
using Shiroi.Unity.Pathfinding2D.Tile;
using UnityEngine;
using Vexe.Runtime.Extensions;

namespace Shiroi.Unity.Pathfinding2D.Util {
    public static class GravityUtil {
        public const float DefaultGravityForce = 9.8f;

        public static List<Vector2> CalculatePath(Vector2 pos, Vector2 initialSpeed, float timeIncrementation, TileMap tileMap,
            IGroundEntity entity, out Node finalNode, float gravityForce = GravityUtil.DefaultGravityForce) {
            finalNode = null;
            var rb = entity.RigidBody;
            var gravity = rb.mass * -gravityForce * rb.gravityScale;
            var list = new List<Vector2>();
            var time = 0F;
            while (finalNode == null) {
                var y = pos.y + initialSpeed.y * time + gravity * Mathf.Pow(time, 2) / 2;
                var x = pos.x + initialSpeed.x * time;
                if (tileMap.IsOutOfBounds(x, y)) {
                    break;
                }
                var newPos = new Vector2(x, y);
                if (!list.IsEmpty()) {
                    var last = list.Last();
                    var dir = newPos - last;
                    var raycast = Physics2D.Raycast(last, dir, dir.magnitude, tileMap.WorldMask);
                    if (raycast) {
                        var hitPoint = raycast.point;
                        list.Add(hitPoint);
                        var hitNode = tileMap.GetNode(hitPoint);
                        if (hitNode != null) {
                            var nodePos = hitNode.Position;
                            var hitDirVec = hitPoint - nodePos;
                            var direc = Direction.FromVector(hitDirVec, 0.4f, 0.4f);
                            finalNode = tileMap.GetNode(hitNode, direc);
                            if (finalNode != null) {
                                list.Add(finalNode.Position);
                            }
                        }
                        break;
                    }
                }
                list.Add(newPos);
                time += timeIncrementation;
            }
            return list;
        }

        public static void DrawGizmos(List<Vector2> path) {
            for (var i = 1; i < path.Count; i++) {
                var point = path[i];
                var previous = path[i - 1];
                Gizmos.DrawLine(previous, point);
            }
        }

        public static float CalculateDistance(List<Vector2> path) {
            float distance = 0;
            for (var i = 1; i < path.Count - 1; i++) {
                var point = path[i];
                var previous = path[i - 1];
                distance += Vector2.Distance(point, previous);
            }
            return distance;
        }
    }
}