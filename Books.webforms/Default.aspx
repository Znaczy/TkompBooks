﻿<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="Books.webforms._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Biblioteka</h1>
        <p class="lead">Prosta aplikacja WWW zbierająca dane o książkach i ich autorach</p>
    </div>

    <div class="row">
        <div class="col-md-8 col-md-offset-2">
            <h2>Książki</h2>

            <!-- <input type="submit" value="Dodaj nową książkę" class="btn btn-default" /> -->
			<asp:Button runat="server" ID="btnAddNew" CssClass="btn btn-default" OnCommand="AddNewBook" Text="Dodaj nową książkę"/>

			<asp:Repeater runat="server" Id="AuthorsRepeater">
			<HeaderTemplate>
				<table class="" style="margin-top:15px">
				<!-- TODO: Style this table using bootstrap CSS classes http://getbootstrap.com/docs/3.3/components/ -->
				<tr>
					<th style="width:100px">Autor</th>
					<th style="width:100px">Tytuł</th>
					<th style="width:100px">Data wydania</th>
					<th style="width:30px"></th>
					<th style="width:30px"></th>
				</tr>
                
				<tr>
					<!-- TODO: add search bar like in authors.aspx -->
					<!-- modyfikuje -->
					<td style="width:100px"><asp:TextBox runat="server" Id="FindBox_Author"></asp:TextBox></td>
					<td style="width:100px"><asp:TextBox runat="server" Id="FindBox_Title"></asp:TextBox></td>
					<td style="width:100px"></td>
					<td style="width:100px"></td>
				</tr>
			</HeaderTemplate>
			<ItemTemplate>
				<tr>
					<td><%# Eval("FirstName") + " " + Eval("LastName") %></td>
					<td><%# Eval("Title") %></td>
					<td><%# Eval("ReleaseDate", "{0:yyyy}") %></td>
					<td><asp:LinkButton ID="lkbEdit" runat="server" OnCommand="EditBook" CommandArgument='<%# Eval("BookId") %>'> E </asp:LinkButton></td>
					<td><asp:LinkButton ID="lkbDelete" runat="server" OnCommand="DeleteBook" CommandArgument='<%# Eval("BookId") %>' OnClientClick="return confirm('Are You sure to delete?')"> X </asp:LinkButton></td>
				</tr>
			</ItemTemplate>
			<FooterTemplate>
			            </table>
			</FooterTemplate>
			</asp:Repeater>
			<% if IsEditMode then %>
				<hr />
					<div class="form-group">
						<label for="Editbox_firstName">Imię</label>
						<input type="text" class="form-control" id="Editbox_firstName" name="Editbox_firstName" placeholder="Imię" value="<%=EditedBook.FirstName%>">
					</div>
					<div class="form-group">
						<label for="Editbox_lastName">Nazwisko</label>
						<input type="text" class="form-control" id="Editbox_lastName" name="Editbox_lastName" placeholder="Nazwisko"value="<%=EditedBook.LastName%>">
					</div>
					<div class="form-group">
						<label for="Editbox_title">Tytuł</label>
						<input type="text" class="form-control" id="Editbox_title" name="Editbox_title" placeholder="Tytuł"value="<%=EditedBook.Title%>">
					</div>
					<div class="form-group">
						<label for="Editbox_date">Data</label>
						<input type="text" class="form-control" id="Editbox_date" name="Editbox_date" placeholder="Data"value="<%=EditedBook.ReleaseDate.Year%>">
					</div>
					<input type="hidden" id="Editbox_id" name="Editbox_id" placeholder="Nazwisko"value="<%=EditedBook.BookId%>">
					<asp:Button runat="server" ID="btnEditSave" CssClass="btn btn-default" OnCommand="SaveBook" Text="Zapisz" OnClientClick="return validateAuthors();"/>
			<%       End if%>

            <!-- TODO: first save these data to DB, To Table `books`. Then get and display books data like in authors.aspx file. Add also methods for Edit, Delete and Add new Books -->
            <!--
				<tr>
                <td>Bolesław Prus</td>
                <td>Faraon</td>
                <td>1980</td>
                <td> E </td>
                <td> X </td>
            </tr>
        
            <tr>
                <td>Stefan Żeromski</td>
                <td>Przedwiośnie</td>
                <td>1980</td>
                <td> E </td>
                <td> X </td>
            </tr>
                <tr>
                    <td>Juliusz Słowacki</td>
                    <td>Kordian</td>
                    <td>1930</td>
                    <td> E </td>
                    <td> X </td>
                </tr>
                <tr>
                    <td>Adam Mickiewicz</td>
                    <td>Dziady</td>
                    <td>1930</td>
                    <td> E </td>
                    <td> X </td>
                </tr>
                <tr>
                    <td>Zofia Nałkowska</td>
                    <td>Granica</td>
                    <td>1930</td>
                    <td> E </td>
                    <td> X </td>
                </tr>
				-->
        </div>
    </div>

</asp:Content>
