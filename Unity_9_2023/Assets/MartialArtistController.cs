
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MartialArtistController : MonoBehaviour
{
    private Animator animator;
    private Transform otherParent;
    private Transform targetParent;
    private Image image;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        // image.
        List<Transform> items = new List<Transform>();
        image.fillMethod = Image.FillMethod.Vertical;
        image.fillOrigin = (int)Image.Origin360.Top;
        foreach (var item in items)
        {
            item.SetParent(otherParent);
        }
        items = items.OrderBy(i => Random.Range(0, 1)).ToList();
        foreach (var item in items)
        {
            item.SetParent(targetParent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("attack");
        }
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetFloat("move_speed", 1);
        }
        else
        {
            animator.SetFloat("move_speed", 0);
        }
    }

    public void Attack(string s)
    {
        Debug.Log("Animation event " + s);/*  */
    }
}
