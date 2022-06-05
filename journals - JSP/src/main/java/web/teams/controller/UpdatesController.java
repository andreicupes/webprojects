package web.teams.controller;
import web.teams.domain.*;
import web.teams.model.DBManager;
import javax.servlet.*;
import javax.servlet.http.*;
import org.json.JSONArray;
import org.json.JSONObject;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.Objects;

public class UpdatesController extends HttpServlet {

    @Override
    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        DBManager dbManager = new DBManager();

        String uname= (String) request.getSession().getAttribute("user");

        String journal = request.getParameter("journal");
        request.getSession().setAttribute("journal", journal);
        int journalid = dbManager.getJournalID(journal);
        request.getSession().setAttribute("journalid", journalid);

        String prevList= (String) request.getSession().getAttribute("prevlist");
        String currList = dbManager.getChanges(uname);
        String toDisplay;
        if(!Objects.equals(prevList, currList)){
        String interval = this.getNotifString(prevList,currList);
        toDisplay = dbManager.getDiff(interval);}
        else
        {toDisplay= "no changes were found";}
        System.out.println(prevList);
        System.out.println(currList);
        System.out.println(toDisplay);
        request.getSession().setAttribute("toDisplay", toDisplay);
        request.getSession().setAttribute("prevlist", currList);



//
        response.sendRedirect("main_page.jsp");
    }

    @Override
    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {

    }

    public String getNotifString(String prev, String curr){
        String c = "(";
        for(int i = prev.length(); i < curr.length()-1; i++)
        {
            if(curr.charAt(i) != ',')
            {
                c += curr.charAt(i) ;
                if (curr.charAt(i+1)  == ',') c += ",";
            }
        }
        c += curr.charAt(curr.length()-1);
        c += ")";
        return c;


    }
}