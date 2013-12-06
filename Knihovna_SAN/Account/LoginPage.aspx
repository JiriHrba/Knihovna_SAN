<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginPage.aspx.cs" Inherits="Knihovna_SAN.Account.LoginPage" MasterPageFile="~/Site.Master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Prihlaseni</h1>
<hr />

<table>

    <tr>
        <td colspan="3">
            <asp:Label ID="LabelLoginInfo" runat="server" Text=""></asp:Label></td>
    </tr>

    <tr>
        <td>Login</td>
        <td>
            <asp:TextBox ID="TxtBoxLogin" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="Uzivatelske jmeno musi byt vyplneno" 
                ControlToValidate="TxtBoxLogin"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>Heslo</td>
        <td>
            <asp:TextBox ID="TxtBoxPassword" runat="server" TextMode="Password"></asp:TextBox>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ErrorMessage="Heslo musi byt vyplneno" ControlToValidate="TxtBoxPassword"></asp:RequiredFieldValidator>
           
        </td>
    </tr>
</table>
<br />
 <asp:Button ID="BtnLogin" runat="server" Text="Prihlasit se" 
        onclick="BtnLogin_Click" />

<h1>Registrace</h1>
<hr />
Registraci muzete provest 
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Client/NewClient.aspx">zde.</asp:HyperLink>







</asp:Content>
