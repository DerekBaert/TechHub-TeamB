using UnityEngine;

public class ParallaxController : MonoBehaviour
{

    [SerializeField]
    private float scrollSpeed = -1.5f;

    [SerializeField]
    private SpriteRenderer _sprite;

    [Range(0.1f, 1f)]
    public float parallaxFactor = 1f;

    private float spriteLength;
    private Vector3 startPosition;

    void Start()
    {
        spriteLength = _sprite.GetComponent<SpriteRenderer>().bounds.size.x;
        startPosition = transform.position;
    }

    void Update()
    {
        float finalSpeed = scrollSpeed * parallaxFactor;

        transform.Translate(finalSpeed * Time.deltaTime, 0, 0);
        if (transform.position.x < startPosition.y - spriteLength)
        {
            transform.position += new Vector3(spriteLength, 0, 0);
        }
    }
}
