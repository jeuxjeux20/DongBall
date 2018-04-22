using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnnouncerManager : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        StartCoroutine(AnnouncementCoroutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator AnnouncementCoroutine()
    {
        while (true)
        {
            if (annoucements.Count == 0)
            {
                yield return new WaitUntil(() => { return annoucements.Count != 0; });
            }
            else
            {
                var announce = annoucements.ConsumeNextPriority();
                AudioSource source = gameObject.GetComponent<AudioSource>();
                if (announce is GenericAnnoucement || announce is MultiKillAnnouncement) // soon 
                {
                    GameObject obj = (GameObject)Instantiate(Resources.Load("Announcer/AnnouncerGeneric"), GameObject.Find("Canvas").transform);
                    obj.GetComponent<Text>().text = announce.Text;
                    PlaySound(announce, source);
                    yield return new WaitForSeconds(2f);
                    obj.GetComponent<Animator>().Play("Done");
                    if (annoucements.Count > 0)
                    {
                        yield return new WaitForSeconds(1f);
                    }
                }
            }
        }
    }

    private static void PlaySound(Annoucement announce, AudioSource source)
    {
        if (source.clip.name == announce.Type.ToString())
        {
            source.Play();
        }
        else
        {
            try
            {
                var sound = Resources.Load("Announcer/Sounds/" + announce.Type.ToString());
                if (sound == null)
                {
                    throw new Exception("The sound couldn't be found sry m8 but you need to add it !!");
                }
                var castedSound = sound as AudioClip;
                if (castedSound == null)
                {
                    Debug.LogError("idk but i cannot cast this (" + announce.Type.ToString() + ") to an audio clip :( : it is a " + sound.GetType());
                    throw new Exception("Cannot cast :(");
                }
                source.clip = castedSound;
                source.Play();
            }
            catch (Exception e)
            {
                Debug.LogError("Something bad has occured while loading the sound: " + e.Message);
            }
        }
    }

    public enum AnnouncementType
    {
        EnemySlain,
        ItselfSlain,
        DoubleKill,
        TripleKill,
        QuadraKill,
        PentaKill,
        HexaKill,
        Other
    }

    public abstract class Annoucement : IPriorityHolder
    {
        public string Text { get; set; }
        public AnnouncementType Type { get; set; }
        public int Priority { get; set; }
        /*
         * Priority 0 = u hav slein an enemi - othar
         * Priority 1 = u hav ben slain 
         * Priority 2-6 = Double, Triple, Quadra, Penta, Hexakill
         */

        public static GenericAnnoucement FromEnemySlain()
        {
            return new GenericAnnoucement("u hav slein an enemi !!", AnnouncementType.EnemySlain, 0);
        }
        public static GenericAnnoucement FromItselfSlain()
        {
            return new GenericAnnoucement("u hav ben slain", AnnouncementType.ItselfSlain, 1);
        }
        public Annoucement()
        {

        }
    }

    public class GenericAnnoucement : Annoucement
    {
        public GenericAnnoucement(string text, AnnouncementType type = AnnouncementType.Other, int priority = 0) : base()
        {
            Text = text;
            Type = type;
            Priority = priority;
        }
        public GenericAnnoucement() : base()
        {

        }
    }

    public class MultiKillAnnouncement : Annoucement
    {
        public int Kills { get; private set; }
        public MultiKillAnnouncement(int kills)
        {
            if (kills < 2)
            {
                throw new ArgumentException("It is not a correct multikill : " + kills, "kills");
            }
            if (kills > 6)
            {
                kills = 6;
            }
            switch (kills)
            {
                case 2:
                    Type = AnnouncementType.DoubleKill;
                    Text = "DOBEL KIL !";
                    break;
                case 3:
                    Type = AnnouncementType.TripleKill;
                    Text = "TRIPEL KIL !";
                    break;
                case 4:
                    Type = AnnouncementType.QuadraKill;
                    Text = "QUADRA KIL !";
                    break;
                case 5:
                    Type = AnnouncementType.PentaKill;
                    Text = "PINTAKIL !";
                    break;
                case 6:
                    Type = AnnouncementType.HexaKill;
                    Text = "HEXAKIL !";
                    break;
                default:
                    throw new Exception("Wat, HOW DID U GET IN DEFAULT");
            }
            Priority = kills;
        }
    }

    private AnnouncerQueue annoucements = new AnnouncerQueue();

    private DateTime lastKill = DateTime.MinValue;
    private int killCount = 0;

    public void ShowEnemySlain()
    {
        if (killCount == 0 || DateTime.Now > lastKill + TimeSpan.FromSeconds(10))
        {
            killCount = 1;
            annoucements.Add(Annoucement.FromEnemySlain());
        } 
        else
        {
            killCount++;
            annoucements.Add(new MultiKillAnnouncement(killCount));
        }
        lastKill = DateTime.Now;
    }
    public void ShowItselfSlain()
    {
        annoucements.Add(Annoucement.FromItselfSlain());
    }
}
