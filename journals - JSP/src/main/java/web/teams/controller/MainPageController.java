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
public class MainPageController extends HttpServlet {

    @Override
    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        DBManager dbManager = new DBManager();

        String uname= (String) request.getSession().getAttribute("user");

        String journal = request.getParameter("journal");
        request.getSession().setAttribute("journal", journal);
        int journalid = dbManager.getJournalID(journal);
        request.getSession().setAttribute("journalid", journalid);



//
        response.sendRedirect("main_page.jsp");
    }

    @Override
    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        DBManager dbManager = new DBManager();
        String uname= (String) request.getSession().getAttribute("user");
        String journal = request.getParameter("journalName");
        request.getSession().setAttribute("journal", journal);

        String summary = request.getParameter("summary");

        dbManager.addArticleToJournals(uname,journal,summary);
        int journalid = dbManager.getJournalID(journal);
        request.getSession().setAttribute("journalid", journalid);

        response.sendRedirect("main_page.jsp");
    }
}