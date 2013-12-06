<%@ Page Language="C#" AutoEventWireup="true" Inherits="Knihovna_SAN.Action.ActionReservation" MasterPageFile="~/Site.Master" CodeFile="ActionReservation.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h1>Rezervace na akci</h1>
<hr />



    <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="622px" 
        AutoGenerateRows="False" CellPadding="4" DataSourceID="ActionDetailDataSource" 
        ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
        <EditRowStyle BackColor="#999999" />
        <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
        <Fields>
            <asp:BoundField DataField="action_date" HeaderText="Datum konani" 
                SortExpression="action_date" />
            <asp:BoundField DataField="action_capacity" HeaderText="Kapacita" 
                SortExpression="action_capacity" />
            <asp:BoundField DataField="action_name" HeaderText="Nazev akce" 
                SortExpression="action_name" />
            <asp:BoundField DataField="action_cost" HeaderText="Cena [Kc]" 
                SortExpression="action_cost" />
        </Fields>
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
    </asp:DetailsView>



    <asp:ObjectDataSource ID="ActionDetailDataSource" runat="server" 
        DataObjectTypeName="DatabaseLibrary.Action" DeleteMethod="Delete" 
        InsertMethod="Insert" SelectMethod="SelectOne" 
        TypeName="DatabaseLibrary.ActionTable" UpdateMethod="Update">
        <DeleteParameters>
            <asp:Parameter Name="id" Type="Int32" />
        </DeleteParameters>
        <SelectParameters>
            <asp:SessionParameter Name="ActionId" SessionField="actionId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>

    <asp:Label ID="LabelInfo" runat="server" Text=""></asp:Label>

    <br />
    <asp:Button ID="BtnActionReservation" runat="server" 
        Text="Potvrdit rezervaci na tuto akci" onclick="BtnActionReservation_Click" />

    

    <asp:Button ID="BtnCancelReservation" runat="server" Text="Zrusit rezervaci" 
        onclick="BtnCancelReservation_Click" />

    

</asp:Content>
