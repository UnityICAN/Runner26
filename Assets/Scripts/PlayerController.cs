using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float forwardSpeed = 1f;
    [SerializeField] private float sideSpeed = 1f;
    [SerializeField] private float jumpStrength = 10f;

    [SerializeField] private Transform spawnPointTransform;

    [SerializeField] private Transform leftLineTransform;
    [SerializeField] private Transform centerLineTransform;
    [SerializeField] private Transform rightLineTransform;

    private int numberOfCollectiblesCollected = 0;

    private Line currentLine;
    private bool isAnchored;

    private bool isAlive;

    private void Start()
    {
        isAlive = true;
        numberOfCollectiblesCollected = 0;
        currentLine = Line.Center; // On place le joueur sur la ligne centrale au départ
        isAnchored = true;
        rb.MovePosition(
            new Vector3(centerLineTransform.position.x,
            rb.position.y,
            rb.position.z));
    }

    private void Update()
    {
        if (isAlive)
        {
            // Get new current line
            if (Input.GetButtonDown("Left")) // On décale vers la ligne de gauche
            {
                if (currentLine == Line.Center)
                {
                    currentLine = Line.Left;
                    isAnchored = false;
                }
                else if (currentLine == Line.Right)
                {
                    currentLine = Line.Center;
                    isAnchored = false;
                }
            } else if (Input.GetButtonDown("Right")) // On décale vers la ligne de droite
            {
                if (currentLine == Line.Center)
                {
                    currentLine = Line.Right;
                    isAnchored = false;
                }
                else if (currentLine == Line.Left)
                {
                    currentLine = Line.Center;
                    isAnchored = false;
                }
            }

            float horizontalVelocity = 0f;

            // Get corresponding transform
            Transform currentLineTransform = centerLineTransform;
            if (currentLine == Line.Left)
                currentLineTransform = leftLineTransform;
            else if (currentLine == Line.Right)
                currentLineTransform = rightLineTransform;

            if (rb.position.x - currentLineTransform.position.x < 0) // Go right
            {
                if (rb.position.x - currentLineTransform.position.x < -0.1f)
                    horizontalVelocity = sideSpeed;
                else if (!isAnchored)
                {
                    rb.MovePosition(
                        new Vector3(currentLineTransform.position.x,
                        rb.position.y,
                        rb.position.z));
                    isAnchored = true;
                }
            } else // Go left
            {
                if (rb.position.x - currentLineTransform.position.x > 0.1f)
                    horizontalVelocity = -sideSpeed;
                else if (!isAnchored)
                {
                    rb.MovePosition(
                        new Vector3(currentLineTransform.position.x,
                        rb.position.y,
                        rb.position.z));
                    isAnchored = true;
                }
            }

            rb.velocity = new Vector3(horizontalVelocity, // Side movements
                rb.velocity.y, // Don't impede gravity
                forwardSpeed); // Endless movement
        }
        else
        {
            if (Input.GetButtonDown("Jump"))
                ResetPlayer();
        }



        /*if (isAlive)
            rb.velocity = new Vector3(Input.GetAxis("Horizontal") * sideSpeed, // Side movements
                rb.velocity.y, // Don't impede gravity
                forwardSpeed); // Endless movement
        else
        {
            if (Input.GetButtonDown("Jump"))
                ResetPlayer();
        }*/
    }

    private void ResetPlayer()
    {
        transform.position = spawnPointTransform.position;
        isAlive = true;
        numberOfCollectiblesCollected = 0;
    }

    private void Reset()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void HandleDeath()
    {
        Debug.Log(numberOfCollectiblesCollected);
        isAlive = false;
        rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
    }

    private void HandleCoinCollection(GameObject coin)
    {
        numberOfCollectiblesCollected++;
        Destroy(coin);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
            HandleDeath();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Collectible"))
            HandleCoinCollection(other.gameObject);

    }
}
