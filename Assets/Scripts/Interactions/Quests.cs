using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Quest
{
    public bool isActive;

    public string title;
    public string description;
}

//{
//    [System.Serializable]

//    public struct Info
//    {
//        public string Name;
//        public Sprite Icon;
//        public string Description;
//    }
    
//    [Header("info")] public Info Information;

//    [System.Serializable]
//    public struct Stat
//    {
//        public int XP;
//        public int Currency;
//    }

//    [Header("Reward")] public Stat Reward = new Stat { Currency = 10, XP = 10 };

    
//    public bool Completed { get; protected set; }
//    public QuestCompletedEvent QuestCompleted;

//    public abstract class QuestGoal : ScriptableObject
//    {
//        protected string Description;
//        public int CurrentAmount { get; protected set; }
//        public int RequiredAmount = 1;

//        public bool Completed { get; protected set; }
//        [HideInInspector] public UnityEvent GoalCompleted;
//        public virtual string GetDescription()
//        {
//            return Description;
//        }
//        public virtual void Intitalize()
//        {
//            Completed = false;
//            GoalCompleted = new UnityEvent();
//        }
//        protected void Evaluate()
//        {
//             if(CurrentAmount >= RequiredAmount) 
//            {
//                Complete();
//            }
//        }
//        public void Complete()
//        {
//            Completed = true;
//            GoalCompleted.Invoke();
//            GoalCompleted.RemoveAllListeners();
//        }

//    }
    
//    public List<QuestGoal> Goals;

//    public void Initialize()
//    {
//        Completed = false;
//        QuestCompleted = new QuestCompletedEvent();

//        foreach(var goal in Goals)
//        {
//            goal.Intitalize();
//            goal.GoalCompleted.AddListener(delegate { CheckGoals(); });
//        }      
//    }
//    private void CheckGoals()
//    {
//        Completed = Goals.All(g => g.Completed);
//        if(Completed)
//        {
//            QuestCompleted.Invoke(this);
//            QuestCompleted.RemoveAllListeners();
//        }
//    }
//}
//public class QuestCompletedEvent : UnityEvent<Quest>
//{

//}
