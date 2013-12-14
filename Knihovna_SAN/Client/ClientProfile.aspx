<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="ClientProfile.aspx.cs" Inherits="Knihovna_SAN.Client.ClientProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h1>Zakladni informace</h1>
<hr/>

    <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" 
    Width="670px" AutoGenerateRows="False" CellPadding="4" 
    DataSourceID="ClientDetailDataSource1" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
        <EditRowStyle BackColor="#999999" />
        <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
        <Fields>
            <asp:BoundField DataField="client_name" HeaderText="Jmeno" 
                SortExpression="client_name" />
            <asp:BoundField DataField="client_surname" HeaderText="Prijmeni" 
                SortExpression="client_surname" />
            <asp:BoundField DataField="client_email" HeaderText="Email" 
                SortExpression="client_email" />
            <asp:BoundField DataField="client_phone" HeaderText="Telefon" 
                SortExpression="client_phone" />
            <asp:BoundField DataField="client_birth_date" HeaderText="Datum narozeni" 
                SortExpression="client_birth_date" />
            <asp:BoundField DataField="client_member_from" HeaderText="Clenstvi od" 
                SortExpression="client_member_from" />
            <asp:BoundField DataField="client_member_to" HeaderText="Clenstvi do" 
                SortExpression="client_member_to" />
            <asp:BoundField DataField="client_street" HeaderText="Ulice" 
                SortExpression="client_street" />
            <asp:BoundField DataField="client_city" HeaderText="Mesto" 
                SortExpression="client_city" />
            <asp:BoundField DataField="client_zip" HeaderText="PSC" 
                SortExpression="client_zip" />
            <asp:BoundField DataField="client_country" HeaderText="Zeme" 
                SortExpression="client_country" />
            <asp:CheckBoxField DataField="client_is_active" HeaderText="Aktivni clenstvi" 
                SortExpression="client_is_active" />
        </Fields>
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
    </asp:DetailsView>

<h1>Rezervovane akce</h1>
<hr/>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" CellPadding="4" 
        DataSourceID="ClientsActionObjectDataSource1" ForeColor="#333333" 
        GridLines="None" Width="881px">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="action_date" HeaderText="Datum konani" 
                SortExpression="action_date" />
            <asp:BoundField DataField="action_capacity" HeaderText="Kapacita" 
                SortExpression="action_capacity" />
            <asp:BoundField DataField="action_name" HeaderText="Nazev" 
                SortExpression="action_name" />
            <asp:BoundField DataField="action_cost" HeaderText="Cena" 
                SortExpression="action_cost" />
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>

    <asp:ObjectDataSource ID="ClientsActionObjectDataSource1" runat="server" 
        DataObjectTypeName="DatabaseLibrary.Action" DeleteMethod="Delete" 
        InsertMethod="Insert" SelectMethod="SelectClientsActions" 
        TypeName="DatabaseLibrary.ActionTable" UpdateMethod="Update">
        <DeleteParameters>
            <asp:Parameter Name="id" Type="Int32" />
        </DeleteParameters>
        <SelectParameters>
            <asp:SessionParameter Name="clientId" SessionField="clientId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>



<asp:ObjectDataSource ID="ClientDetailDataSource1" runat="server" 
    DataObjectTypeName="DatabaseLibrary.Client" DeleteMethod="Delete" 
    InsertMethod="Insert" SelectMethod="SelectOne" 
    TypeName="DatabaseLibrary.ClientTable" UpdateMethod="Update">
    <DeleteParameters>
        <asp:Parameter Name="clientId" Type="Int32" />
    </DeleteParameters>
    <SelectParameters>
        <asp:SessionParameter Name="clientId" SessionField="clientId" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>

<h1>Rezervované knihy</h1>
</br>
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="reservation_id" DataSourceID="ODS_clientReservations">
        <Columns>
            <asp:BoundField DataField="book_name" HeaderText="Název knihy" 
                SortExpression="book_name" />
            <asp:BoundField DataField="reservation_date" HeaderText="Datum rezervace" 
                SortExpression="reservation_date" />
            <asp:BoundField DataField="reservation_appeal" HeaderText="K vyzvednutí" 
                SortExpression="reservation_appeal" />
            <asp:CommandField ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ODS_clientReservations" runat="server" 
        DeleteMethod="Delete" SelectMethod="SelectAllByClient" 
        TypeName="DatabaseLibrary.ReservationTable">
        <DeleteParameters>
            <asp:Parameter Name="reservationId" Type="Int32" />
        </DeleteParameters>
        <SelectParameters>
            <asp:SessionParameter DefaultValue="" Name="clientId" SessionField="clientId" 
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
<hr/>

<h1>Aktuální sankce</h1>
<hr/>

</asp:Content>
