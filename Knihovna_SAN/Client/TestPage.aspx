<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeFile="TestPage.aspx.cs" Inherits="Knihovna_SAN.Client.TestPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        TestPage</p>
    <p>
        --------------------------------------------------------</p>
    <p>
        Category</p>
    <table>
        <tr>
            <td>
                CategoryName
            </td>
            <td>
                <asp:TextBox ID="TextBox_categoryName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                CategoryType
            </td>
            <td>
                <asp:TextBox ID="TextBox_categoryType" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button1" runat="server" Text="Pridat do kategorie" OnClick="Button1_Click" />
            </td>
        </tr>
    </table>
    <br />
    <br />
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataCategorySource1">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowSelectButton="True" />
            <asp:BoundField DataField="category_id" HeaderText="category_id" SortExpression="category_id" />
            <asp:BoundField DataField="category_name" HeaderText="category_name" SortExpression="category_name" />
            <asp:BoundField DataField="category_type" HeaderText="category_type" SortExpression="category_type" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataCategorySource1" runat="server" DataObjectTypeName="DatabaseLibrary.Category"
        DeleteMethod="Delete" InsertMethod="InsertCategory" SelectMethod="SelectAll"
        TypeName="DatabaseLibrary.CategoryTable" UpdateMethod="Update">
        <DeleteParameters>
            <asp:Parameter Name="categoryId" Type="Int32" />
        </DeleteParameters>
    </asp:ObjectDataSource>
    <p>
        &nbsp;</p>
    <p>
        --------------------------------------------------------</p>
    <p>
        Action</p>
    <table>
        <tr>
            <td>
                ActionDate
            </td>
            <td>
                <asp:TextBox ID="TextBox_actionDate" runat="server"></asp:TextBox>
                //datum ve tvaru dd/mm/yyyy (s lomitky)
            </td>
        </tr>
        <tr>
            <td>
                ActionCapacity
            </td>
            <td>
                <asp:TextBox ID="TextBox_actionCapacity" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                ActionName
            </td>
            <td>
                <asp:TextBox ID="TextBox_actionName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                ActionCost
            </td>
            <td>
                <asp:TextBox ID="TextBox_actionCost" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button2" runat="server" Text="Pridat do akce" OnClick="Button2_Click" />
            </td>
        </tr>
    </table>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectActionDataSource">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowSelectButton="True" />
            <asp:BoundField DataField="action_id" HeaderText="action_id" SortExpression="action_id" />
            <asp:BoundField DataField="action_date" HeaderText="action_date" SortExpression="action_date" />
            <asp:BoundField DataField="action_capacity" HeaderText="action_capacity" SortExpression="action_capacity" />
            <asp:BoundField DataField="action_name" HeaderText="action_name" SortExpression="action_name" />
            <asp:BoundField DataField="action_cost" HeaderText="action_cost" SortExpression="action_cost" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectActionDataSource" runat="server" DataObjectTypeName="DatabaseLibrary.Action"
        DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="SelectAll" TypeName="DatabaseLibrary.ActionTable"
        UpdateMethod="Update">
        <DeleteParameters>
            <asp:Parameter Name="id" Type="Int32" />
        </DeleteParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_book" runat="server" DataObjectTypeName="DatabaseLibrary.Book"
        InsertMethod="InsertBook" SelectMethod="SelectAll" TypeName="DatabaseLibrary.BookTable"
        DeleteMethod="Delete" UpdateMethod="Update">
        <DeleteParameters>
            <asp:Parameter Name="bookId" Type="Int32" />
        </DeleteParameters>
    </asp:ObjectDataSource>
    <p>
        --------------------------------------------------------</p>
    <p>
        Vytisk/Copy</p>
    <table>
        <tr>
            <td>
                Je pritomen
            </td>
            <td>
                <asp:TextBox ID="TextBox_copy_isPresent" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                ID knihy
            </td>
            <td>
                <asp:DropDownList ID="ddl_copy_bookID" runat="server" DataSourceID="ODS_book" DataTextField="book_name"
                    DataValueField="book_id">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="addCopy" runat="server" OnClick="Button3_Click" Text="Pridat vytisk" />
            </td>
        </tr>
    </table>
    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataCopySource1">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowSelectButton="True" />
            <asp:BoundField DataField="copy_id" HeaderText="copy_id" SortExpression="copy_id" />
            <asp:BoundField DataField="copy_is_present" HeaderText="copy_is_present" SortExpression="copy_is_present" />
            <asp:BoundField DataField="book_id" HeaderText="book_id" SortExpression="book_id" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataCopySource1" runat="server" DataObjectTypeName="DatabaseLibrary.Copy"
        DeleteMethod="Delete" InsertMethod="InsertCopy" SelectMethod="SelectAll" TypeName="DatabaseLibrary.CopyTable"
        UpdateMethod="Update">
        <DeleteParameters>
            <asp:Parameter Name="copyId" Type="Int32" />
        </DeleteParameters>
    </asp:ObjectDataSource>
    <br />
    <p>
        --------------------------------------------------------</p>
    <p>
        Rezervace</p>
    <table>
    <tr>
            <td>
                Reservation_date
            </td>
            <td>
                <asp:TextBox ID="TextBox_reservation_date" runat="server"></asp:TextBox>
                //datum ve tvaru dd/mm/yyyy (s lomitky)
            </td>
        </tr>
        <tr>
            <td>
                Reservation_appeal
            </td>
            <td>
                <asp:TextBox ID="TextBox_reservation_appeal" runat="server"></asp:TextBox>
                //datum ve tvaru dd/mm/yyyy (s lomitky)
            </td>
        </tr>
        <tr>
            <td>
                Klient (ID)
            </td>
            <td>
                <asp:DropDownList ID="ddl_reser_client" runat="server" DataSourceID="ODS_client"
                    DataTextField="client_login" DataValueField="client_id">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                ID vytisku
            </td>
            <td>
                <asp:DropDownList ID="ddl_reser_copy" runat="server" DataSourceID="ODS_copy" DataTextField="copy_id"
                    DataValueField="copy_id">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Pridat rezervaci" />
            </td>
        </tr>
    </table>
    <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataReservationSource1">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowSelectButton="True" />
            <asp:BoundField DataField="reservation_id" HeaderText="reservation_id" SortExpression="reservation_id" />
            <asp:BoundField DataField="reservation_date" HeaderText="reservation_date" SortExpression="reservation_date" />
            <asp:BoundField DataField="reservation_appeal" HeaderText="reservation_appeal" SortExpression="reservation_appeal" />
            <asp:BoundField DataField="client_id" HeaderText="client_id" SortExpression="client_id" />
            <asp:BoundField DataField="copy_id" HeaderText="copy_id" SortExpression="copy_id" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataReservationSource1" runat="server" DataObjectTypeName="DatabaseLibrary.Reservation"
        DeleteMethod="Delete" InsertMethod="InsertReservation" SelectMethod="SelectAll"
        TypeName="DatabaseLibrary.ReservationTable" UpdateMethod="Update">
        <DeleteParameters>
            <asp:Parameter Name="reservationId" Type="Int32" />
        </DeleteParameters>
    </asp:ObjectDataSource>
    <br />
    <asp:ObjectDataSource ID="ODS_client" runat="server" DataObjectTypeName="DatabaseLibrary.Client"
        DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="SelectAll" TypeName="DatabaseLibrary.ClientTable">
        <DeleteParameters>
            <asp:Parameter Name="clientId" Type="Int32" />
        </DeleteParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_copy" runat="server" DataObjectTypeName="DatabaseLibrary.Copy"
        DeleteMethod="Delete" InsertMethod="InsertCopy" SelectMethod="SelectAll" TypeName="DatabaseLibrary.CopyTable"
        UpdateMethod="Update">
        <DeleteParameters>
            <asp:Parameter Name="copyId" Type="Int32" />
        </DeleteParameters>
    </asp:ObjectDataSource>
    <br />
    <p>
        --------------------------------------------------------</p>
    <p>
        Sanction</p>
    <table>
        <tr>
            <td>
                stype_ammount
            </td>
            <td>
                <asp:TextBox ID="TextBox_stype_ammount" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                stype_name
            </td>
            <td>
                <asp:TextBox ID="TextBox_stype_name" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="Pridat typ sankce" />
            </td>
        </tr>
    </table>
    <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSanctionSource1">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowSelectButton="True" />
            <asp:BoundField DataField="stype_id" HeaderText="stype_id" SortExpression="stype_id" />
            <asp:BoundField DataField="stype_ammount" HeaderText="stype_ammount" SortExpression="stype_ammount" />
            <asp:BoundField DataField="stype_name" HeaderText="stype_name" SortExpression="stype_name" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSanctionSource1" runat="server" DataObjectTypeName="DatabaseLibrary.SanctionType"
        DeleteMethod="Delete" InsertMethod="InsertSanctionType" SelectMethod="SelectAll"
        TypeName="DatabaseLibrary.SanctionTypeTable" UpdateMethod="Update">
        <DeleteParameters>
            <asp:Parameter Name="stypeId" Type="Int32" />
        </DeleteParameters>
    </asp:ObjectDataSource>
    <p>
        --------------------------------------------------------</p>
    <p>
        SanctionHistory</p>
    <table>
        <tr>
            <td>
                sanction_grant
            </td>
            <td>
                <asp:TextBox ID="TextBox_sanction_grant" runat="server"></asp:TextBox>
                //datum ve tvaru dd/mm/yyyy (s lomitky)
            </td>
        </tr>
        <tr>
            <td>
                sanction_paid
            </td>
            <td>
                <asp:TextBox ID="TextBox_sanction_paid" runat="server"></asp:TextBox>
                //datum ve tvaru dd/mm/yyyy (s lomitky)
            </td>
        </tr>
        <tr>
            <td>
                sanction_desc
            </td>
            <td>
                <asp:TextBox ID="TextBox_sanction_desc" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                client_id
            </td>
            <td>
                <asp:DropDownList ID="DropDownList_sanction_client_id" runat="server" DataSourceID="ODS_client"
                    DataTextField="client_login" DataValueField="client_id">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                stype_id
            </td>
            <td>
                <asp:DropDownList ID="DropDownList_sanction_stype_id" runat="server" DataSourceID="ODS_stype"
                    DataTextField="stype_name" DataValueField="stype_id">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button6" runat="server" OnClick="Button6_Click" Text="Pridat sanction history" />
                <asp:ObjectDataSource ID="ODS_stype" runat="server" SelectMethod="SelectAll" TypeName="DatabaseLibrary.SanctionTypeTable">
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
    <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSanctionHistorySource1">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowSelectButton="True" />
            <asp:BoundField DataField="sanction_id" HeaderText="sanction_id" SortExpression="sanction_id" />
            <asp:BoundField DataField="sanction_grant" HeaderText="sanction_grant" SortExpression="sanction_grant" />
            <asp:BoundField DataField="sanction_paid" HeaderText="sanction_paid" SortExpression="sanction_paid" />
            <asp:BoundField DataField="sanction_desc" HeaderText="sanction_desc" SortExpression="sanction_desc" />
            <asp:BoundField DataField="client_id" HeaderText="client_id" SortExpression="client_id" />
            <asp:BoundField DataField="stype_id" HeaderText="stype_id" SortExpression="stype_id" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSanctionHistorySource1" runat="server" DataObjectTypeName="DatabaseLibrary.SanctionsHistory"
        DeleteMethod="Delete" InsertMethod="InsertSanctionsHistory" SelectMethod="SelectAll"
        TypeName="DatabaseLibrary.SanctionsHistoryTable" UpdateMethod="Update">
        <DeleteParameters>
            <asp:Parameter Name="sanctionId" Type="Int32" />
        </DeleteParameters>
    </asp:ObjectDataSource>
    <br />
    <p>
        --------------------------------------------------------</p>
    <p>
        Author</p>
    <table>
        <tr>
            <td>
                author_name
            </td>
            <td>
                <asp:TextBox ID="TextBox_author_name" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                author_surname
            </td>
            <td>
                <asp:TextBox ID="TextBox_author_surname" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                author_middle_name
            </td>
            <td>
                <asp:TextBox ID="TextBox_author_middle_name" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                author_birth_date
            </td>
            <td>
                <asp:TextBox ID="TextBox_author_birth_date" runat="server"></asp:TextBox>
                //datum ve tvaru dd/mm/yyyy (s lomitky)
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button7" runat="server" OnClick="Button7_Click" Text="Pridat autora" />
            </td>
        </tr>
    </table>
    <asp:GridView ID="GridView7" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataAuthorSource1">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowSelectButton="True" />
            <asp:BoundField DataField="author_id" HeaderText="author_id" SortExpression="author_id" />
            <asp:BoundField DataField="author_name" HeaderText="author_name" SortExpression="author_name" />
            <asp:BoundField DataField="author_surname" HeaderText="author_surname" SortExpression="author_surname" />
            <asp:BoundField DataField="author_middle_name" HeaderText="author_middle_name" SortExpression="author_middle_name" />
            <asp:BoundField DataField="author_birth_date" HeaderText="author_birth_date" SortExpression="author_birth_date" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataAuthorSource1" runat="server" DataObjectTypeName="DatabaseLibrary.Author"
        DeleteMethod="Delete" InsertMethod="InsertAuthor" SelectMethod="SelectAll" TypeName="DatabaseLibrary.AuthorTable"
        UpdateMethod="Update">
        <DeleteParameters>
            <asp:Parameter Name="reservationId" Type="Int32" />
        </DeleteParameters>
    </asp:ObjectDataSource>
    <p>
        --------------------------------------------------------</p>
    <p>
        Book</p>
    <table>
        <tr>
            <td>
                book_name
            </td>
            <td>
                <asp:TextBox ID="TextBox_book_name" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                book_isbn
            </td>
            <td>
                <asp:TextBox ID="TextBox_book_isbn" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                book_annotation
            </td>
            <td>
                <asp:TextBox ID="TextBox_book_annotation" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                author_id
            </td>
            <td>
                <asp:DropDownList ID="DropDownList_book_author_id" runat="server" DataSourceID="ODS_author"
                    DataTextField="author_surname" DataValueField="author_id">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button8" runat="server" OnClick="Button8_Click" Text="Pridat knihu" />
                <asp:ObjectDataSource ID="ODS_author" runat="server" SelectMethod="SelectAll" TypeName="DatabaseLibrary.AuthorTable">
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
    <asp:GridView ID="GridView8" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataBookSource1">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowSelectButton="True" />
            <asp:BoundField DataField="book_id" HeaderText="book_id" SortExpression="book_id" />
            <asp:BoundField DataField="book_name" HeaderText="book_name" SortExpression="book_name" />
            <asp:BoundField DataField="book_isbn" HeaderText="book_isbn" SortExpression="book_isbn" />
            <asp:BoundField DataField="book_annotation" HeaderText="book_annotation" SortExpression="book_annotation" />
            <asp:BoundField DataField="author_id" HeaderText="author_id" SortExpression="author_id" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataBookSource1" runat="server" DataObjectTypeName="DatabaseLibrary.Book"
        DeleteMethod="Delete" InsertMethod="InsertBook" SelectMethod="SelectAll" TypeName="DatabaseLibrary.BookTable"
        UpdateMethod="Update">
        <DeleteParameters>
            <asp:Parameter Name="bookId" Type="Int32" />
        </DeleteParameters>
    </asp:ObjectDataSource>
    <p>
        --------------------------------------------------------</p>
    <p>
        Borrowing</p>
    <table>
        <tr>
            <td>
                borrowing_from
            </td>
            <td>
                <asp:TextBox ID="TextBox_borrowing_from" runat="server"></asp:TextBox>
                //datum ve tvaru dd/mm/yyyy (s lomitky)
            </td>
        </tr>
        <tr>
            <td>
                borrowing_to
            </td>
            <td>
                <asp:TextBox ID="TextBox_borrowing_to" runat="server"></asp:TextBox>
                //datum ve tvaru dd/mm/yyyy (s lomitky)
            </td>
        </tr>
        <tr>
            <td>
                borrowing_is_returned
            </td>
            <td>
                <asp:TextBox ID="TextBox_borrowing_is_returned" runat="server"></asp:TextBox>
                // zadat bud false nebo true
            </td>
        </tr>
        <tr>
            <td>
                client_id
            </td>
            <td>
                <asp:DropDownList ID="DropDownList_borrowing_client_id" runat="server" DataSourceID="ODS_client"
                    DataTextField="client_surname" DataValueField="client_id">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                copy_id
            </td>
            <td>
                <asp:DropDownList ID="DropDownList_borrowing_copy_id" runat="server" DataSourceID="ODS_copy"
                    DataTextField="copy_id" DataValueField="copy_id">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button9" runat="server" OnClick="Button9_Click" Text="Pridat knihu" />
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="GridView9" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataBorrowingSource1">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowSelectButton="True" />
            <asp:BoundField DataField="borrowing_id" HeaderText="borrowing_id" SortExpression="borrowing_id" />
            <asp:BoundField DataField="borrowing_from" HeaderText="borrowing_from" SortExpression="borrowing_from" />
            <asp:BoundField DataField="borrowing_to" HeaderText="borrowing_to" SortExpression="borrowing_to" />
            <asp:CheckBoxField DataField="borrowing_is_returned" HeaderText="borrowing_is_returned"
                SortExpression="borrowing_is_returned" />
            <asp:BoundField DataField="client_id" HeaderText="client_id" SortExpression="client_id" />
            <asp:BoundField DataField="copy_id" HeaderText="copy_id" SortExpression="copy_id" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataBorrowingSource1" runat="server" DataObjectTypeName="DatabaseLibrary.Borrowing"
        DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="SelectAll" TypeName="DatabaseLibrary.BorrowingTable"
        UpdateMethod="Update">
        <DeleteParameters>
            <asp:Parameter Name="id" Type="Int32" />
        </DeleteParameters>
    </asp:ObjectDataSource>
    <p>
        --------------------------------------------------------</p>
    <p>
        BookCategory</p>
    <table>
        <tr>
            <td>
                category_id
            </td>
            <td>
                <asp:DropDownList ID="DropDownList_bookCatogery_category_id" runat="server" DataSourceID="ODS_category"
                    DataTextField="category_name" DataValueField="category_id">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                book_id
            </td>
            <td>
                <asp:DropDownList ID="DropDownList_bookCatogery_book_id" runat="server" DataSourceID="ODS_book"
                    DataTextField="book_name" DataValueField="book_id">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button10" runat="server" OnClick="Button10_Click" Text="Pridat do BookCategory - vazebni tabulka" />
            </td>
        </tr>
    </table>
    <asp:GridView ID="GridView10" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataBookCategorySource1">
        <Columns>
            <asp:BoundField DataField="category_id" HeaderText="category_id" SortExpression="category_id" />
            <asp:BoundField DataField="book_id" HeaderText="book_id" SortExpression="book_id" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataBookCategorySource1" runat="server" DataObjectTypeName="DatabaseLibrary.BookCategory"
        InsertMethod="InsertBookCategory" SelectMethod="SelectAll" TypeName="DatabaseLibrary.BookCategoryTable">
    </asp:ObjectDataSource>
    <p>
        --------------------------------------------------------</p>
    <p>
        ActionCategory</p>
    <table>
        <tr>
            <td>
                action_id
            </td>
            <td>
                <asp:DropDownList ID="DropDownList_actionCategory_action_id" runat="server" DataSourceID="ODS_action"
                    DataTextField="action_name" DataValueField="action_id">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                category_id
            </td>
            <td>
                <asp:DropDownList ID="DropDownList_actionCategory_category_id" runat="server" DataSourceID="ODS_category"
                    DataTextField="category_name" DataValueField="category_id">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button11" runat="server" OnClick="Button11_Click" Text="Pridat do ActionCategory - vazebni tabulka" />
            </td>
        </tr>
    </table>
    <asp:GridView ID="GridView11" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataActionCategorySource1">
        <Columns>
            <asp:BoundField DataField="action_id" HeaderText="action_id" SortExpression="action_id" />
            <asp:BoundField DataField="category_id" HeaderText="category_id" SortExpression="category_id" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataActionCategorySource1" runat="server" SelectMethod="SelectAll"
        TypeName="DatabaseLibrary.ActionCategoryTable"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_action" runat="server" SelectMethod="SelectAll" TypeName="DatabaseLibrary.ActionTable">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_category" runat="server" SelectMethod="SelectAll" TypeName="DatabaseLibrary.CategoryTable">
    </asp:ObjectDataSource>
</asp:Content>
