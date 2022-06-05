package web.teams.domain;

public class Article {
    private int id;
    private String user;
    private int journalid;
    
    private String summary;
    private String date;

    public Article(){}
    public Article(int a, int b, String c, String d, String e)
    {
        this.id = a;
        this.journalid = b;
        this.summary = c;
        this.user = d;
        this.date = e;
    }

    public int getId() {
        return id;
    }

    public int getJournalid() {
        return journalid;
    }

    public String getSummary() {
        return summary;
    }

    public String getdate() {
        return date;
    }

    public String getuser() {
        return user;
    }

    public void setjournalid(int journalid) {
        this.journalid = journalid;
    }

    public void setsummary(String summary) {
        this.summary = summary;
    }

    public void setId(int id) {
        this.id = id;
    }

    public void setuser(String user) {
        this.user = user;
    }

    public void setdate(String date) {
        this.date = date;
    }
}
