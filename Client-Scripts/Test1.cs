using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : MonoBehaviour
{
    /*
    public   Camera camera1;
    private Transform thispoint;
    private Vector2 way;
    private LineRenderer LR;
    public List<Transform> points = new List<Transform>();
    // Use this for initialization
    void Start()
    {
        camera1 = Camera.main;
        LR = this.GetComponent<LineRenderer>();
        thispoint = this.transform;
        thispoint.position = new Vector2(Random.Range(-9.0f, 9.0f), Random.Range(-5.0f, 5.0f));
        way = new Vector2(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        way = way.normalized;
        LR.positionCount=30;
        LR.startWidth=1.0f;
        LR.endWidth = 2.0f;
    }

    private void FixedUpdate()
    {
        thispoint.Translate(way * gamectrl.speed * Time.fixedDeltaTime);

    }
    private void Update()
    {
        int i = 1;
        checkside();
        LR.SetPosition(0, thispoint.position);
        foreach (Transform tr in points)
        {
            LR.SetPosition(i * 2 - 1, tr.position);
            if (i == 15) break;
            LR.SetPosition(i * 2, thispoint.position);
            i++;
        }
        for (i = (i - 1) * 2; i <= 29; i++)
        {
            LR.SetPosition(i, thispoint.position);
        }
    }
    private void checkside()
    {
        if (Mathf.Abs(thispoint.position.x) >= 9 || Mathf.Abs(thispoint.position.y) >= 5)
        {
            if (thispoint.position.x >= 9)
            {
                way = (2 * Vector2.Dot(-way, new Vector2(-1, 0)) * new Vector2(-1, 0) + way).normalized;
            }
            else if (thispoint.position.x <= -9)
            {
                way = (2 * Vector2.Dot(-way, new Vector2(-1, 0)) * new Vector2(-1, 0) + way).normalized;
            }
            else if (thispoint.position.y >= 5)
            {
                way = (2 * Vector2.Dot(-way, new Vector2(0, -1)) * new Vector2(0, -1) + way).normalized;
            }
            else if (thispoint.position.y <= -5)
            {
                way = (2 * Vector2.Dot(-way, new Vector2(0, 1)) * new Vector2(0, 1) + way).normalized;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("OnCollisionEnter");
        points.Add(collision.transform);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        print("OnCollisionExit");
        int i = 0;
        foreach (Transform ts in points)
        {

            if (collision.gameObject.name == ts.name)
            {
                if (points.Count == 1) { points.Remove(points[points.Count - 1]); return; }
                for (int n = i; n < points.Count - 1; n++)
                {
                    points[n] = points[n + 1];

                }
                points.Remove(points[points.Count - 1]);
                return;
            }
            i++;
        }
    }*/
    public List<Vector3> points = new List<Vector3>();
    private LineRenderer LR;
    public Transform m_Transform;
    public ParticleSystem m_ParticleSystem;
   [SerializeField]  public ParticleSystem.Particle[] m_particles;

    void Start()
    {
        LR = this.GetComponent<LineRenderer>();
        m_Transform = gameObject.GetComponent<Transform>();
        m_ParticleSystem = gameObject.GetComponent<ParticleSystem>();
        m_particles = new ParticleSystem.Particle[m_ParticleSystem.main.maxParticles];  //实例化，个数为粒子系统设置的最大粒子数
        int num = m_ParticleSystem.GetParticles(m_particles);
        //设置粒子移动.
        for (int i = 0; i < num; i++)
        {
          //  print(m_particles[i].position);
             points.Add(m_particles[i].position);
        }
        LR.positionCount = 30;
        LR.startWidth = 0.005f;
        LR.endWidth = 0.009f;
    }

    void Update()
    {
        int num = m_ParticleSystem.GetParticles(m_particles);
        points.Clear();
        for (int i = 0; i < num; i++)
        {
           // print(m_particles[i].position);
            points.Add(m_particles[i].position);
        }
       for (int i = 0; i < 30; i++)
        {
            LR.SetPosition(i, points[i]);
        }


    }
}
