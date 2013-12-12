<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="Knihovna_SAN.Account.ForgotPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

Pokud jste zapomneli sve heslo, do pole nize zadejte Vas email, ktery jste zadali pri registraci. Na tento email Vam 
bude zaslano nove vygenerovane heslo.

<table>
    <tr>
        <td>Email</td>
        <td><asp:TextBox ID="TxtBoxRegEmail" runat="server"></asp:TextBox></td>
        <td>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                ControlToValidate="TxtBoxRegEmail" Display="Dynamic" 
                ErrorMessage="Emailova adresa neni platna." 
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
     </tr>
     <tr>
        <td colspan="3">
            <asp:Button ID="BtnSendNewPass" runat="server" onclick="BtnSendNewPass_Click" 
                Text="Odeslat heslo" />
         </td>
     </tr>
     <tr>
        <td colspan="3">
            <asp:Label ID="LabelInfo" runat="server" Text=""></asp:Label>
         </td>
     </tr>
</asp:Content>
