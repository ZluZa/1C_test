using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private Player _player;
    
    private PolygonCollider2D _collider; 
    private UICornerCut _cut; 
    private Vector3[] _points; 
    private Vector2[] _colliderPoints;


   private void Awake()
   {
       _collider = GetComponent<PolygonCollider2D>();
       _cut = GetComponent<UICornerCut>();
   }

   private void Update()
   { 
       _points = new Vector3[4];
       _colliderPoints = new Vector2[4];
         GetComponent<RectTransform>().GetLocalCorners(_points);
         _colliderPoints[0] = new Vector2(-_cut.cornerSize.x, _cut.cornerSize.y);
         _colliderPoints[1] = new Vector2(_cut.cornerSize.x, _cut.cornerSize.y);

         _collider.SetPath(0, _colliderPoints);
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
       if (other.gameObject.layer == 10)
       {
           if (_player != null)
           {
              _player.AddEnemyInSight(other.GetComponent<Enemy>());
           } 
       }
   }

   private void OnTriggerExit2D(Collider2D other)
   {
       if (other.gameObject.layer == 10)
       {
           if (_player != null)
           {
               _player.RemoveEnemyInSight(other.GetComponent<Enemy>());
           } 
       }
   }
}
