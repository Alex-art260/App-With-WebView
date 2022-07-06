using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class MovementObjects : MonoBehaviour, IDetectSameObjects
{
    public static event Action OnMultiplyScore;

    [SerializeField] private List<GameObject> _groupObjectsOne = new List<GameObject>();
    [SerializeField] private List<GameObject> _groupObjectsTwo = new List<GameObject>();
    [SerializeField] private int _speedOneLine;
    [SerializeField] private int _speedTwoLine;
    [SerializeField] private float _boardXOneLine;
    [SerializeField] private float _boardXTwoLine;
    [SerializeField] private SpriteContainer _spriteContainer;
    public List<GameObject> GroupObjectOne => _groupObjectsOne;
    public List<GameObject> GroupObjectTwo => _groupObjectsTwo;

    private List<ObjectView> objectView = new List<ObjectView>();

    private void Update()
    {
        MoveObjects(_groupObjectsOne, _boardXOneLine, _speedOneLine);
        MoveObjects(_groupObjectsTwo, _boardXTwoLine, _speedTwoLine);

        DetectSameObjects();
    }

    private void MoveObjects(List<GameObject> groupObjects, float boardX, int speed)
    {
        bool condition;
        float step = 1.67f;

        for (int i = 0; i < groupObjects.Count; i++)
        {
            groupObjects[i].transform.Translate(Vector3.right * Time.deltaTime * speed);

            var conditionOne = groupObjects[i].transform.localPosition.x > boardX;
            var conditionTwo = groupObjects[i].transform.localPosition.x < boardX;

            if (speed > 0)
            {
                step = -step;
                condition = conditionOne;
            }
            else
            {
                step = +step;
                condition = conditionTwo;
            }

            if (condition)
            {
                var item2 = groupObjects[i];
                groupObjects.RemoveAt(i);
                groupObjects.Insert(0, item2);

                groupObjects[0].transform.position = new Vector3(groupObjects[1].transform.position.x + step, 
                    groupObjects[1].transform.position.y, groupObjects[1].transform.position.z);

                _spriteContainer.ChangeSprite(groupObjects, 0);
            }
        }
    }

    public void DetectSameObjects()
    {
        for (int i = 0; i < _groupObjectsOne.Count; i++)
        {
            for (int j = 0; j < _groupObjectsTwo.Count; j++)
            {
                var groupCenterOne = _groupObjectsOne[i].gameObject.transform.position.x;
                var groupCenterTwo = _groupObjectsTwo[j].gameObject.transform.position.x;

                var groupDetectOne = _groupObjectsOne[i].GetComponent<ObjectView>();
                var groupDetectTwo = _groupObjectsTwo[j].GetComponent<ObjectView>();

                var dist = groupCenterOne - groupCenterTwo;

                if (dist <= 0.01014233 && dist >= 0 && groupDetectOne.ID == groupDetectTwo.ID)
                {
                    ScoreCount.instance.MultiplyScore();
                }
            }
        }
    }

}
