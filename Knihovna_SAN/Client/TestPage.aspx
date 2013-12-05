<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="TestPage.aspx.cs" Inherits="Knihovna_SAN.Client.TestPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <p> TestPage</p>
    <p> --------------------------------------------------------</p>
    <p> Category</p>
    <table>
        <tr>
            <td>CategoryName</td><td><asp:TextBox ID="TextBox_categoryName" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
            <td>CategoryType</td><td><asp:TextBox ID="TextBox_categoryType" runat="server"></asp:TextBox></td>
            </tr>
            <tr><td><asp:Button ID="Button1" runat="server" Text="Pridat do kategorie" 
                    onclick="Button1_Click" /></td></tr>        
    </table>
    

    <p> --------------------------------------------------------</p>
    <p> Action</p>
    <table>
        <tr>
            <td>ActionDate</td><td><asp:TextBox ID="TextBox_actionDate" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
            <td>ActionCapacity</td><td><asp:TextBox ID="TextBox_actionCapacity" runat="server"></asp:TextBox></td>
            </tr>
            <tr><td>ActionName</td><td><asp:TextBox ID="TextBox_actionName" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
            <td>ActionCost</td><td><asp:TextBox ID="TextBox_actionCost" runat="server"></asp:TextBox></td>
            </tr>
            <tr><td><asp:Button ID="Button2" runat="server" Text="Pridat do akce" 
                    onclick="Button2_Click" /></td></tr>        
    </table>


    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataSourceID="ObjectActionDataSource">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" 
                ShowSelectButton="True" />
            <asp:BoundField DataField="action_id" HeaderText="action_id" 
                SortExpression="action_id" />
            <asp:BoundField DataField="action_date" HeaderText="action_date" 
                SortExpression="action_date" />
            <asp:BoundField DataField="action_capacity" HeaderText="action_capacity" 
                SortExpression="action_capacity" />
            <asp:BoundField DataField="action_name" HeaderText="action_name" 
                SortExpression="action_name" />
            <asp:BoundField DataField="action_cost" HeaderText="action_cost" 
                SortExpression="action_cost" />
        </Columns>
    </asp:GridView>
   

    <asp:ObjectDataSource ID="ObjectActionDataSource" runat="server" 
        DataObjectTypeName="DatabaseLibrary.Action" DeleteMethod="Delete" 
        InsertMethod="Insert" SelectMethod="SelectAll" 
        TypeName="DatabaseLibrary.ActionTable" UpdateMethod="Update">
        <DeleteParameters>
            <asp:Parameter Name="id" Type="Int32" />
        </DeleteParameters>
    </asp:ObjectDataSource>
   

    <br />
    <br />
    <asp:Button ID="Button3" runat="server" onclick="Button3_Click" 
        Text="Pridat vytisk" />
    <br />
    <br />
    <asp:Button ID="Button4" runat="server" onclick="Button4_Click" 
        Text="Pridat rezervaci" />
   

    <br />
    <br />
    <asp:Button ID="Button5" runat="server" onclick="Button5_Click" 
        Text="Pridat typ sankce" />
    <br />
    <br />
    <asp:Button ID="Button6" runat="server" onclick="Button6_Click" 
        Text="Pridat sanction history" />
    <br />
    <br />
   

</asp:Content>
