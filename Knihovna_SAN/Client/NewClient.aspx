<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="NewClient.aspx.cs" Inherits="Knihovna_SAN.Client.NewClient" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Registrace noveho klienta</h1><hr/>

    <table>

        <tr>
            <td>Jmeno</td>
            <td><asp:TextBox ID="TxtBoxName" runat="server"></asp:TextBox></td>
            <td><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ErrorMessage="Tuto polozku musite vyplnit." 
                    ControlToValidate="TxtBoxName"></asp:RequiredFieldValidator></td>
            
        </tr>

         <tr>
            <td>Prijmeni</td>
            <td><asp:TextBox ID="TxtBoxSurname" runat="server" ></asp:TextBox></td>
            <td><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ErrorMessage="Tuto polozku musite vyplnit." 
                    ControlToValidate="TxtBoxSurname"></asp:RequiredFieldValidator></td>
        </tr>

         <tr>
            <td>Email</td>
            <td><asp:TextBox ID="TxtBoxEmail" runat="server"></asp:TextBox></td>
            <td><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ErrorMessage="Tuto polozku musite vyplnit."  
                    ControlToValidate="TxtBoxEmail" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ControlToValidate="TxtBoxEmail" Display="Dynamic" 
                    ErrorMessage="Emailova adresa neni platna." 
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
             </td>
        </tr>

         <tr>
            <td>Telefon</td>
            <td><asp:TextBox ID="TxtBoxPhone" runat="server"></asp:TextBox></td>
            
        </tr>

         <tr>
            <td>Datum narozeni</td>
            <td><asp:TextBox ID="TxtBoxBirthDate" runat="server"></asp:TextBox></td>
            <td><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ErrorMessage="Tuto polozku musite vyplnit ve tvaru DD/MM/YYYY." 
                     ControlToValidate="TxtBoxBirthDate" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                    ControlToValidate="TxtBoxBirthDate" Display="Dynamic" 
                    ErrorMessage="Datum musi byt ve tvaru DD/MM/YYYY." 
                    ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[/](0[1-9]|1[012])[/](19|20)[0-9]{2}$"></asp:RegularExpressionValidator>
             </td>
        </tr>

         <tr>
            <td>Ulice</td>
            <td><asp:TextBox ID="TxtBoxStreet" runat="server"></asp:TextBox></td>
            
        </tr>

         <tr>
            <td>Mesto</td>
            <td><asp:TextBox ID="TxtBoxCity" runat="server"></asp:TextBox></td>
            
        </tr>

         <tr>
            <td>PSC</td>
            <td><asp:TextBox ID="TxtBoxZIP" runat="server"></asp:TextBox></td>
            
        </tr>

        <tr>
            <td>Stat</td>
            <td><asp:TextBox ID="TxtBoxCountry" runat="server"></asp:TextBox>
              
            </td>
            
        </tr>

      

        <tr>
            <td>Login</td>
            <td><asp:TextBox ID="TxtBoxLogin" runat="server" ></asp:TextBox></td>
            <td><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                    ErrorMessage="Tuto polozku musite vyplnit." 
                    ControlToValidate="TxtBoxLogin"></asp:RequiredFieldValidator></td>
        </tr>


        <tr>
            <td colspan="3"><asp:Label ID="LabelInfo" runat="server"></asp:Label></td>
        </tr>


    </table>
    
      <asp:Button ID="BtnInsertClient" runat="server" Text="Vlozit noveho klienta" onclick="BtnInsertClient_Click" />




</asp:Content>
