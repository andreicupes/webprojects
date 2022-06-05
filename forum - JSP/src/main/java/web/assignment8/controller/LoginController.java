package web.assignment8.controller;

import web.assignment8.model.DBManager;
import web.assignment8.domain.User;

import javax.servlet.*;
import javax.servlet.http.*;
import javax.servlet.annotation.*;
import java.io.IOException;

//@WebServlet(name = "LoginController", value = "/LoginController")
public class LoginController extends HttpServlet {

    public LoginController(){super();}


    @Override
    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        String username = request.getParameter("username");
        String password = request.getParameter("password");
        RequestDispatcher rd = null;

        DBManager dbmanager = new DBManager();
        User user = dbmanager.authenticate(username, password);


        if (user != null) {
            HttpSession session = request.getSession();
            session.setAttribute("user", user);
            session.setAttribute("fail",false);
            dbmanager.addUserToSession(user.getId(), user.getId());
            request.getSession().setAttribute("topicsString",dbmanager.getTopics());

            response.sendRedirect("main_page.jsp");

        } else {
            HttpSession session = request.getSession();
            session.setAttribute("fail","login_issue");

            response.sendRedirect("index.jsp");
        }


    }
}