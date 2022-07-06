using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteContainer : MonoBehaviour
{
    [SerializeField] public List<Sprite> sprites;
    [SerializeField] private MovementObjects _movementObjects;

    private void Awake()
    {
        InitializeSpriteInObjects(_movementObjects.GroupObjectOne);
        InitializeSpriteInObjects(_movementObjects.GroupObjectTwo);
    }

    private void InitializeSpriteInObjects(List<GameObject> gameObjects)
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            ChangeSprite(gameObjects, i);
        }
    }

    public void ChangeSprite(List<GameObject> gameObjects, int i)
    {
        var spriteObject = gameObjects[i].GetComponent<SpriteRenderer>();
        var objectView = gameObjects[i].GetComponent<ObjectView>();

        var randomIndex = Random.Range(0, sprites.Count);

        spriteObject.sprite = sprites[randomIndex];
        objectView.ID = randomIndex;
    }
}
