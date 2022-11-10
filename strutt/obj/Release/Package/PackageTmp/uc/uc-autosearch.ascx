<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc-autosearch.ascx.cs"
    Inherits="strutt.uc.uc_autosearch" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<style type="text/css">
    .loading
    {
        background-image: url(images/loading45.gif);
        background-position: right;
        background-repeat: no-repeat;
    }
</style>
<script type="text/javascript">
    function OnClientPopulating(sender, e) {
        sender._element.className = "loading";
    }
    function OnClientCompleted(sender, e) {
        sender._element.className = "";
    }
</script>
<script>
    function ProviderSelectedFunc(sender, args) {
        //here I know I am sending in tb_enrollingProvider1
        var name = document.getElementById("<%=txtAutoComplete.ClientID%>").value
        location.href = ("../../../search.aspx?search=" + name);
    }
</script>
<style type="text/css">
    .completionList
    {
        border: solid 1px Gray;
        margin: 0px;
        padding: 3px;
        max-height: 242px;
        overflow: hidden;
        background-color: #FFFFFF;
        font-size: 11px;
        color: #191919;
    }
    .listItem
    {
        background-color: white;
        padding: 2px;
    }
    .itemHighlighted
    {
        background-color: #f0f0f0;
        padding: 2px;
    }
</style>
<asp:TextBox ID="txtAutoComplete" runat="server" Width="90%" placeholder="quick search" OnClientClick="return autoValidate()" TabIndex="0" />
<cc1:AutoCompleteExtender ID="ACEProducts" runat="server" DelimiterCharacters=""
    Enabled="True" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"
    CompletionListHighlightedItemCssClass="itemHighlighted" ServicePath="~/auto-webservice.asmx"
    ServiceMethod="GetCompletionList" TargetControlID="txtAutoComplete" MinimumPrefixLength="1"
    CompletionInterval="10" EnableCaching="true" CompletionSetCount="12" OnClientItemSelected="ProviderSelectedFunc"
    OnClientHiding="OnClientCompleted" OnClientPopulated="OnClientCompleted" OnClientPopulating="OnClientPopulating">
</cc1:AutoCompleteExtender>
<asp:ImageButton ID="imgBtnSearch" ImageUrl="~/images/search-icon.png" Width="30px"
    TabIndex="0" Height="30px" OnClientClick="ProviderSelectedFunc" runat="server" />
