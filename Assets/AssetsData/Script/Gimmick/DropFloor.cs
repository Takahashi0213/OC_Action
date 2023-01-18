using UnityEngine;

public class DropFloor : MonoBehaviour
{
    private Vector3 StartPosition;
    private bool DropFlag = false;

    public float RespawnY = -50.0f;

    //大きさ変更（あまり賢くない方法）
    private Vector3 DefScale;
    private Vector3 ScaleMove;
    private int ScaleCount = 0;
    private bool ScaleFlag = false;
    private float ScaleSpeed = 60.0f;

    void Start()
    {
        StartPosition = transform.position;
        DefScale = transform.localScale;
        ScaleMove = DefScale / ScaleSpeed;
    }

    void Update()
    {
        //大きさ変更
        if (ScaleFlag)
        {
            ScaleCount++;

            transform.localScale += ScaleMove;
            if (ScaleCount >= (int)ScaleSpeed)
            {
                transform.localScale = DefScale;
                ScaleFlag = false;
            }
        }

        //低さ制限
        if(transform.position.y < -50.0f)
        {
            transform.position = StartPosition;
            GetComponent<Animator>().SetBool("DropFlag", false);
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Animator>().enabled = true;
            transform.localScale = Vector3.zero;
            DropFlag = false;
            ScaleFlag = true;
            ScaleCount = 0;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (DropFlag)
        {
            return;
        }

        //接触したオブジェクトのタグが"Player"のとき
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<Animator>().SetBool("DropFlag", true);
            DropFlag = true;
        }
    }

    public void FloorDrop()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Animator>().enabled = false;
    }
}
