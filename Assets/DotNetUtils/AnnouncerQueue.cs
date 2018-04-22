class AnnouncerQueue : PriorityQueue<AnnouncerManager.Annoucement>
{
    public new void Add(AnnouncerManager.Annoucement a)
    {
        if (a is AnnouncerManager.MultiKillAnnouncement)
        {
            RemoveAll(x => x is AnnouncerManager.MultiKillAnnouncement);
            RemoveAll(x => x.Type == AnnouncerManager.AnnouncementType.EnemySlain);
        }
        base.Add(a);
    } 
}

