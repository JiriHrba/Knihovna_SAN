<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Loans.aspx.cs" Inherits="Knihovna_SAN.Client.Loans" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:ScriptManager ID="sm" runat="server"></asp:ScriptManager>

<asp:UpdatePanel ID="up1" runat="server">
<ContentTemplate>
<h2>
        Zápis výpújčky
    </h2>
        <table>
        <tr>
            <td>
                Datum zapůjčení
            </td>
            <td>
                <asp:TextBox ID="TextBox_borrowing_from" runat="server"></asp:TextBox>
                //datum ve tvaru dd/mm/yyyy (s lomitky)
            </td>
        </tr>
        <tr>
            <td>
                Navrátit do
            </td>
            <td>
                <asp:TextBox ID="TextBox_borrowing_to" runat="server"></asp:TextBox>
                //datum ve tvaru dd/mm/yyyy (s lomitky)
            </td>
        </tr>
        <tr>
            <td>
                ID klienta
            </td>
            <td>
                <asp:DropDownList ID="DropDownList_borrowing_client_id" runat="server" DataSourceID="ODS_client"
                    DataTextField="client_id" DataValueField="client_id">
                </asp:DropDownList>
                <asp:ObjectDataSource ID="ODS_client" runat="server" 
                    DataObjectTypeName="DatabaseLibrary.Client" DeleteMethod="Delete" 
                    InsertMethod="Insert" SelectMethod="SelectAll" 
                    TypeName="DatabaseLibrary.ClientTable" UpdateMethod="Update">
                    <DeleteParameters>
                        <asp:Parameter Name="clientId" Type="Int32" />
                    </DeleteParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
         
                <tr>
                    <td>
                        Název knihy
                    </td>
                    <td>
                
                        <asp:DropDownList ID="dropDownList_book_id" runat="server" DataSourceID="ODS_book" 
                            DataTextField="book_name" DataValueField="book_id" AutoPostBack="True" 
                            onselectedindexchanged="BookSelected">
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="ODS_book" runat="server" 
                            DataObjectTypeName="DatabaseLibrary.Book" DeleteMethod="Delete" 
                            InsertMethod="InsertBook" SelectMethod="SelectAll" 
                            TypeName="DatabaseLibrary.BookTable" UpdateMethod="Update">
                            <DeleteParameters>
                                <asp:Parameter Name="bookId" Type="Int32" />
                            </DeleteParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                <tr>
                    <td>
                        ID výtisku
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList_borrowing_copy_id" runat="server" DataSourceID="ODS_copy"
                            DataTextField="copy_id" DataValueField="copy_id">
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="ODS_copy" runat="server" 
                            DataObjectTypeName="DatabaseLibrary.Copy" DeleteMethod="Delete" 
                            InsertMethod="InsertCopy" SelectMethod="SelectAllPresentByBook" 
                            TypeName="DatabaseLibrary.CopyTable" UpdateMethod="Update">
                            <DeleteParameters>
                                <asp:Parameter Name="copyId" Type="Int32" />
                            </DeleteParameters>
                            <SelectParameters>
                                <asp:ControlParameter ControlID="dropDownList_book_id" Name="bookId" 
                                    PropertyName="SelectedValue" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                
        <tr>
            <td>
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Zapsat vypůjčku" />
                <asp:Button ID="Button2" runat="server" Text="Rezervovat" Enabled="false" 
                    onclick="Button2_Click" />
            </td>
        </tr>
    </table>
    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    <hr />
<h2>
        Vratka knihy&nbsp;
    </h2>
    <table>
        <tr>
            <td>
                Název knihy
            </td>
            <td>
                <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="ODS_book" 
                    DataTextField="book_name" DataValueField="book_id">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="Button_find_by_book" runat="server" 
                    Text="Hledat podle názvu knihy" />
            </td>
        </tr>
        <tr>
            <td>
                ID klienta
            </td>
            <td>
                <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="ODS_client" 
                    DataTextField="client_id" DataValueField="client_id">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="Button_find_by_clientID" runat="server" 
                    Text="Hledat podle ID klienta" onclick="Button_find_by_clientID_Click" />
            </td>
        </tr>
        
    </table>

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataSourceID="ODS_Borrowing" Visible="False">
        <Columns>
            <asp:CommandField ShowEditButton="True" />
            <asp:BoundField DataField="borrowing_id" HeaderText="borrowing_id" 
                SortExpression="borrowing_id" />
            <asp:BoundField DataField="borrowing_from" HeaderText="borrowing_from" 
                SortExpression="borrowing_from" />
            <asp:BoundField DataField="borrowing_to" HeaderText="borrowing_to" 
                SortExpression="borrowing_to" />
            <asp:CheckBoxField DataField="borrowing_is_returned" 
                HeaderText="borrowing_is_returned" SortExpression="borrowing_is_returned" />
            <asp:BoundField DataField="client_id" HeaderText="client_id" 
                SortExpression="client_id" />
            <asp:BoundField DataField="copy_id" HeaderText="copy_id" 
                SortExpression="copy_id" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ODS_Borrowing" runat="server" 
        DataObjectTypeName="DatabaseLibrary.Borrowing" DeleteMethod="Delete" 
        InsertMethod="Insert" SelectMethod="SelectAllbyClientId" 
        TypeName="DatabaseLibrary.BorrowingTable" UpdateMethod="Update">
        <DeleteParameters>
            <asp:Parameter Name="id" Type="Int32" />
        </DeleteParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownList1" Name="clientId" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>

</ContentTemplate> 
</asp:UpdatePanel>

</asp:Content>
