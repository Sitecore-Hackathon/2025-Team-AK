<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bulkoperations.aspx.cs" Inherits="BulkUpdate.bulkoperations" %>
<!DOCTYPE html>
<html>
<head>
    <title>Sitecore Bulk Operations</title>
    <script src="/sitecore/shell/Controls/Tree.js"></script>
    <style>
        body { font-family: Arial, sans-serif; margin: 20px; }
        .actions { margin-top: 20px; }
        .tree-container { height: 400px; overflow-y: scroll; border: 1px solid #ccc; padding: 10px; }
    </style>
</head>
<body>
    <h2>Sitecore Bulk Operations</h2>
    <form id="form1" runat="server">
        <div class="tree-container">
            <asp:TreeView ID="ItemTreeView" runat="server" ShowCheckBoxes="All" />
        </div>
        <div class="actions">
            <asp:Button ID="btnCopy" runat="server" Text="Copy Selected" OnClick="CopyItems" />
            <asp:Button ID="btnMove" runat="server" Text="Move Selected" OnClick="MoveItems" />
            <asp:Button ID="btnDelete" runat="server" Text="Delete Selected" OnClick="DeleteItems" />
        </div>
    </form>
</body>
</html>
