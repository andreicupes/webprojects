package web.teams.domain;

public class Team {
    private int id;
    private int captainID;
    private String name;
    private String description;
    private String members;

    public Team(){}
    public Team(int a,int b, String c, String d, String e)
    {
        this.id = a;
        this.captainID = b;
        this.description = c;
        this.name = d;
        this.members = e;
    }

    public int getId() {
        return id;
    }

    public int getCaptainID() {
        return captainID;
    }

    public String getDescription() {
        return description;
    }

    public String getMembers() {
        return members;
    }

    public String getName() {
        return name;
    }

    public void setCaptainID(int captainID) {
        this.captainID = captainID;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public void setId(int id) {
        this.id = id;
    }

    public void setName(String name) {
        this.name = name;
    }

    public void setMembers(String members) {
        this.members = members;
    }
}
