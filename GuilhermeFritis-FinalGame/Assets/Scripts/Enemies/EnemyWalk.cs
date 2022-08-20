using UnityEngine;

namespace Enemy
{
    public class EnemyWalk : EnemyBase
    {
        [Header("Trajectory")]
        public GameObject[] waypoints;
        public float minDistance = 1f;
        public float speed = 1f;
        public float height = 2f;
        public float center = 0f;
        
        private int _index = 0;

        void Awake()
        {
            foreach (var item in waypoints)
            {
                RaycastHit[] hits = Physics.RaycastAll(item.transform.position, Vector3.down);
                item.transform.position = new Vector3(item.transform.position.x, hits[0].point.y + (height/2) - center, item.transform.position.z);
            }
        }

        public override void Update()
        {
            if(Vector3.Distance(transform.position, waypoints[_index].transform.position) < minDistance)
            {
                _index++;
                if(_index >= waypoints.Length)
                {
                    _index = 0;
                }
            }

            transform.position = Vector3.MoveTowards(transform.position, waypoints[_index].transform.position, Time.deltaTime * speed);
            transform.LookAt(waypoints[_index].transform.position);
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(0.1f, 1f, 0.4f, 1f);
            Vector3 helper = transform.position;
            helper.y += center;
            Gizmos.DrawLine(helper + Vector3.up * height/2, helper - Vector3.up * height/2);
        }
    }
}
