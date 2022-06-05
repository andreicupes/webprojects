package web.teams.controller;
import web.teams.model.DBManager;
import web.teams.domain.*;

import javax.servlet.*;
import javax.servlet.http.*;
import javax.servlet.annotation.*;
import java.io.IOException;
public class LoginController extends HttpServlet {

    public LoginController(){super();}


    @Override
    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        String user = request.getParameter("username");


        DBManager dbmanager = new DBManager();


        if (user != null) {
            HttpSession session = request.getSession();
            session.setAttribute("user", user);


            response.sendRedirect("main_page.jsp");

        } else {
            HttpSession session = request.getSession();
            session.setAttribute("fail","login_issue");

            response.sendRedirect("index.jsp");
        }


    }
}
