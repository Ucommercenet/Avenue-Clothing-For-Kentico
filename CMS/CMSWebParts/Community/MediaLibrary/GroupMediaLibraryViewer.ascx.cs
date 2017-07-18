﻿using System;

using CMS.Community;
using CMS.DocumentEngine.Web.UI;
using CMS.Helpers;
using CMS.PortalEngine.Web.UI;
using CMS.Base;
using CMS.SiteProvider;

public partial class CMSWebParts_Community_MediaLibrary_GroupMediaLibraryViewer : CMSAbstractWebPart
{
    #region "Public properties"

    /// <summary>
    /// Gets or sets WHERE condition.
    /// </summary>
    public string WhereCondition
    {
        get
        {
            return ValidationHelper.GetString(GetValue("WhereCondition"), srcLib.WhereCondition);
        }
        set
        {
            SetValue("WhereCondition", value);
            srcLib.WhereCondition = value;
        }
    }


    /// <summary>
    /// Gets or sets group name.
    /// </summary>
    public string GroupName
    {
        get
        {
            string groupName = ValidationHelper.GetString(GetValue("GroupName"), "");
            if ((string.IsNullOrEmpty(groupName) || groupName == GroupInfoProvider.CURRENT_GROUP) && (CommunityContext.CurrentGroup != null))
            {
                return CommunityContext.CurrentGroup.GroupName;
            }
            if (groupName == GroupInfoProvider.CURRENT_GROUP)
            {
                return QueryHelper.GetString("groupname", "");
            }
            return groupName;
        }
        set
        {
            SetValue("GroupName", value);
        }
    }


    /// <summary>
    /// Gets or sets ORDER BY condition.
    /// </summary>
    public string OrderBy
    {
        get
        {
            return ValidationHelper.GetString(GetValue("OrderBy"), srcLib.OrderBy);
        }
        set
        {
            SetValue("OrderBy", value);
            srcLib.OrderBy = value;
        }
    }


    /// <summary>
    /// Gets or sets top N selected documents.
    /// </summary>
    public int SelectTopN
    {
        get
        {
            return ValidationHelper.GetInteger(GetValue("SelectTopN"), srcLib.TopN);
        }
        set
        {
            SetValue("SelectTopN", value);
            srcLib.TopN = value;
        }
    }


    /// <summary>
    /// Gets or sets selected columns.
    /// </summary>
    public string Columns
    {
        get
        {
            return ValidationHelper.GetString(GetValue("Columns"), srcLib.SelectedColumns);
        }
        set
        {
            SetValue("Columns", value);
            srcLib.SelectedColumns = value;
        }
    }


    /// <summary>
    /// Gets or sets site name.
    /// </summary>
    public string SiteName
    {
        get
        {
            return ValidationHelper.GetString(GetValue("SiteName"), srcLib.SiteName);
        }
        set
        {
            SetValue("SiteName", value);
            srcLib.SiteName = value;
        }
    }


    /// <summary>
    /// Gets or sets the cache item name.
    /// </summary>
    public override string CacheItemName
    {
        get
        {
            return base.CacheItemName;
        }
        set
        {
            base.CacheItemName = value;
            srcLib.CacheItemName = value;
        }
    }


    /// <summary>
    /// Cache dependencies, each cache dependency on a new line.
    /// </summary>
    public override string CacheDependencies
    {
        get
        {
            return ValidationHelper.GetString(base.CacheDependencies, srcLib.CacheDependencies);
        }
        set
        {
            base.CacheDependencies = value;
            srcLib.CacheDependencies = value;
        }
    }


    /// <summary>
    /// Gets or sets the cache minutes.
    /// </summary>
    public override int CacheMinutes
    {
        get
        {
            return base.CacheMinutes;
        }
        set
        {
            base.CacheMinutes = value;
            srcLib.CacheMinutes = value;
        }
    }

    #endregion


    #region "Basic repeater properties"

    /// <summary>
    /// Gets or sets AlternatingItemTemplate property.
    /// </summary>
    public string AlternatingItemTransformationName
    {
        get
        {
            return ValidationHelper.GetString(GetValue("AlternatingItemTransformationName"), "");
        }
        set
        {
            SetValue("AlternatingItemTransformationName", value);
        }
    }


    /// <summary>
    /// Gets or sets FooterTemplate property.
    /// </summary>
    public string FooterTransformationName
    {
        get
        {
            return ValidationHelper.GetString(GetValue("FooterTransformationName"), "");
        }
        set
        {
            SetValue("FooterTransformationName", value);
        }
    }


    /// <summary>
    /// Gets or sets HeaderTemplate property.
    /// </summary>
    public string HeaderTransformationName
    {
        get
        {
            return ValidationHelper.GetString(GetValue("HeaderTransformationName"), "");
        }
        set
        {
            SetValue("HeaderTransformationName", value);
        }
    }


    /// <summary>
    /// Gets or sets ItemTemplate property.
    /// </summary>
    public string TransformationName
    {
        get
        {
            return ValidationHelper.GetString(GetValue("TransformationName"), "");
        }
        set
        {
            SetValue("TransformationName", value);
        }
    }


    /// <summary>
    /// Gets or sets SeparatorTemplate property.
    /// </summary>
    public string SeparatorTransformationName
    {
        get
        {
            return ValidationHelper.GetString(GetValue("SeparatorTransformationName"), "");
        }
        set
        {
            SetValue("SeparatorTransformationName", value);
        }
    }


    /// <summary>
    /// Gets or sets HideControlForZeroRows property.
    /// </summary>
    public bool HideControlForZeroRows
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("HideControlForZeroRows"), true);
        }
        set
        {
            SetValue("HideControlForZeroRows", value);
            repLibraries.HideControlForZeroRows = value;
        }
    }


    /// <summary>
    /// Gets or sets ZeroRowsText property.
    /// </summary>
    public string ZeroRowsText
    {
        get
        {
            return ValidationHelper.GetString(GetValue("ZeroRowsText"), "");
        }
        set
        {
            SetValue("ZeroRowsText", value);
            repLibraries.ZeroRowsText = value;
        }
    }

    #endregion


    #region "UniPager properties"

    /// <summary>
    /// Gets or sets the value that indicates whether pager should be hidden for single page.
    /// </summary>
    public bool HidePagerForSinglePage
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("HidePagerForSinglePage"), pagerElem.HidePagerForSinglePage);
        }
        set
        {
            SetValue("HidePagerForSinglePage", value);
            pagerElem.HidePagerForSinglePage = value;
        }
    }


    /// <summary>
    /// Gets or sets the number of records to display on a page.
    /// </summary>
    public int PageSize
    {
        get
        {
            return ValidationHelper.GetInteger(GetValue("PageSize"), pagerElem.PageSize);
        }
        set
        {
            SetValue("PageSize", value);
            pagerElem.PageSize = value;
        }
    }


    /// <summary>
    /// Gets or sets the number of pages displayed for current page range.
    /// </summary>
    public int GroupSize
    {
        get
        {
            return ValidationHelper.GetInteger(GetValue("GroupSize"), pagerElem.GroupSize);
        }
        set
        {
            SetValue("GroupSize", value);
            pagerElem.GroupSize = value;
        }
    }


    /// <summary>
    /// Gets or sets the pager mode ('querystring' or 'postback').
    /// </summary>
    public string PagingMode
    {
        get
        {
            return ValidationHelper.GetString(GetValue("PagingMode"), "querystring");
        }
        set
        {
            if (value != null)
            {
                SetValue("PagingMode", value);
                switch (value.ToLowerCSafe())
                {
                    case "postback":
                        pagerElem.PagerMode = UniPagerMode.PostBack;
                        break;
                    default:
                        pagerElem.PagerMode = UniPagerMode.Querystring;
                        break;
                }
            }
        }
    }


    /// <summary>
    /// Gets or sets the querysting parameter.
    /// </summary>
    public string QueryStringKey
    {
        get
        {
            return ValidationHelper.GetString(GetValue("QueryStringKey"), pagerElem.QueryStringKey);
        }
        set
        {
            SetValue("QueryStringKey", value);
            pagerElem.QueryStringKey = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether first and last item template are displayed dynamically based on current view.
    /// </summary>
    public bool DisplayFirstLastAutomatically
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("DisplayFirstLastAutomatically"), pagerElem.DisplayFirstLastAutomatically);
        }
        set
        {
            SetValue("DisplayFirstLastAutomatically", value);
            pagerElem.DisplayFirstLastAutomatically = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether first and last item template are displayed dynamically based on current view.
    /// </summary>
    public bool DisplayPreviousNextAutomatically
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("DisplayPreviousNextAutomatically"), pagerElem.DisplayPreviousNextAutomatically);
        }
        set
        {
            SetValue("DisplayPreviousNextAutomatically", value);
            pagerElem.DisplayPreviousNextAutomatically = value;
        }
    }

    #endregion


    #region "UniPager Template properties"

    /// <summary>
    /// Gets or sets the pages template.
    /// </summary>
    public string PagesTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("Pages"), "");
        }
        set
        {
            SetValue("Pages", value);
        }
    }


    /// <summary>
    /// Gets or sets the current page template.
    /// </summary>
    public string CurrentPageTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("CurrentPage"), "");
        }
        set
        {
            SetValue("CurrentPage", value);
        }
    }


    /// <summary>
    /// Gets or sets the separator template.
    /// </summary>
    public string SeparatorTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("PageSeparator"), "");
        }
        set
        {
            SetValue("PageSeparator", value);
        }
    }


    /// <summary>
    /// Gets or sets the first page template.
    /// </summary>
    public string FirstPageTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("FirstPage"), "");
        }
        set
        {
            SetValue("FirstPage", value);
        }
    }


    /// <summary>
    /// Gets or sets the last page template.
    /// </summary>
    public string LastPageTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("LastPage"), "");
        }
        set
        {
            SetValue("LastPage", value);
        }
    }


    /// <summary>
    /// Gets or sets the previous page template.
    /// </summary>
    public string PreviousPageTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("PreviousPage"), "");
        }
        set
        {
            SetValue("PreviousPage", value);
        }
    }


    /// <summary>
    /// Gets or sets the next page template.
    /// </summary>
    public string NextPageTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("NextPage"), "");
        }
        set
        {
            SetValue("NextPage", value);
        }
    }


    /// <summary>
    /// Gets or sets the previous group template.
    /// </summary>
    public string PreviousGroupTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("PreviousGroup"), "");
        }
        set
        {
            SetValue("PreviousGroup", value);
        }
    }


    /// <summary>
    /// Gets or sets the next group template.
    /// </summary>
    public string NextGroupTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("NextGroup"), "");
        }
        set
        {
            SetValue("NextGroup", value);
        }
    }


    /// <summary>
    /// Gets or sets the layout template.
    /// </summary>
    public string LayoutTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("PagerLayout"), "");
        }
        set
        {
            SetValue("PagerLayout", value);
        }
    }


    /// <summary>
    /// Gets or sets the direct page template.
    /// </summary>
    public string DirectPageTemplate
    {
        get
        {
            return ValidationHelper.GetString(GetValue("DirectPage"), "");
        }
        set
        {
            SetValue("DirectPage", value);
        }
    }

    #endregion


    #region "Overridden methods"

    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();

        SetupControl();
    }


    /// <summary>
    /// Reload data override.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        SetupControl();
    }


    /// <summary>
    /// OnPreRender override.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        if (!StopProcessing)
        {
            if (RequestHelper.IsPostBack() && (pagerElem.PagerMode == UniPagerMode.PostBack))
            {
                // Make sure that the pager (in postback mode) propagates the correct current page value
                pagerElem.RebindPager();
                repLibraries.DataBind();
            }

            if (!repLibraries.HasData() && HideControlForZeroRows)
            {
                Visible = false;
            }
        }

        base.OnPreRender(e);
    }


    /// <summary>
    /// Clears cache.
    /// </summary>
    public override void ClearCache()
    {
        srcLib.ClearCache();
    }

    #endregion


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        repLibraries.DataBindByDefault = false;
        pagerElem.PageControl = repLibraries.ID;

        if (StopProcessing)
        {
            // Do nothing
        }
        else
        {
            // Basic control properties
            repLibraries.HideControlForZeroRows = HideControlForZeroRows;
            repLibraries.ZeroRowsText = ZeroRowsText;

            // Data source properties
            srcLib.WhereCondition = WhereCondition;
            srcLib.OrderBy = OrderBy;
            srcLib.TopN = SelectTopN;
            srcLib.CacheItemName = CacheItemName;
            srcLib.CacheDependencies = CacheDependencies;
            srcLib.CacheMinutes = CacheMinutes;
            srcLib.SelectedColumns = Columns;
            srcLib.SiteName = SiteName;

            // Set group ID
            srcLib.GroupID = -1;
            if (!string.IsNullOrEmpty(GroupName))
            {
                GroupInfo gi = GroupInfoProvider.GetGroupInfo(GroupName, (String.IsNullOrEmpty(SiteName) ? SiteContext.CurrentSiteName : SiteName));
                if (gi != null)
                {
                    srcLib.GroupID = gi.GroupID;
                }
            }


            #region "Repeater template properties"

            // Apply transformations if they exist
            if (!String.IsNullOrEmpty(TransformationName))
            {
                repLibraries.ItemTemplate = TransformationHelper.LoadTransformation(this, TransformationName);
            }
            if (!String.IsNullOrEmpty(AlternatingItemTransformationName))
            {
                repLibraries.AlternatingItemTemplate = TransformationHelper.LoadTransformation(this, AlternatingItemTransformationName);
            }
            if (!String.IsNullOrEmpty(FooterTransformationName))
            {
                repLibraries.FooterTemplate = TransformationHelper.LoadTransformation(this, FooterTransformationName);
            }
            if (!String.IsNullOrEmpty(HeaderTransformationName))
            {
                repLibraries.HeaderTemplate = TransformationHelper.LoadTransformation(this, HeaderTransformationName);
            }
            if (!String.IsNullOrEmpty(SeparatorTransformationName))
            {
                repLibraries.SeparatorTemplate = TransformationHelper.LoadTransformation(this, SeparatorTransformationName);
            }

            #endregion


            // UniPager properties
            pagerElem.PageSize = PageSize;
            pagerElem.GroupSize = GroupSize;
            pagerElem.QueryStringKey = QueryStringKey;
            pagerElem.DisplayFirstLastAutomatically = DisplayFirstLastAutomatically;
            pagerElem.DisplayPreviousNextAutomatically = DisplayPreviousNextAutomatically;
            pagerElem.HidePagerForSinglePage = HidePagerForSinglePage;

            switch (PagingMode.ToLowerCSafe())
            {
                case "postback":
                    pagerElem.PagerMode = UniPagerMode.PostBack;
                    break;
                default:
                    pagerElem.PagerMode = UniPagerMode.Querystring;
                    break;
            }


            #region "UniPager template properties"

            // UniPager template properties
            if (!String.IsNullOrEmpty(PagesTemplate))
            {
                pagerElem.PageNumbersTemplate = TransformationHelper.LoadTransformation(pagerElem, PagesTemplate);
            }

            if (!String.IsNullOrEmpty(CurrentPageTemplate))
            {
                pagerElem.CurrentPageTemplate = TransformationHelper.LoadTransformation(pagerElem, CurrentPageTemplate);
            }

            if (!String.IsNullOrEmpty(SeparatorTemplate))
            {
                pagerElem.PageNumbersSeparatorTemplate = TransformationHelper.LoadTransformation(pagerElem, SeparatorTemplate);
            }

            if (!String.IsNullOrEmpty(FirstPageTemplate))
            {
                pagerElem.FirstPageTemplate = TransformationHelper.LoadTransformation(pagerElem, FirstPageTemplate);
            }

            if (!String.IsNullOrEmpty(LastPageTemplate))
            {
                pagerElem.LastPageTemplate = TransformationHelper.LoadTransformation(pagerElem, LastPageTemplate);
            }

            if (!String.IsNullOrEmpty(PreviousPageTemplate))
            {
                pagerElem.PreviousPageTemplate = TransformationHelper.LoadTransformation(pagerElem, PreviousPageTemplate);
            }

            if (!String.IsNullOrEmpty(NextPageTemplate))
            {
                pagerElem.NextPageTemplate = TransformationHelper.LoadTransformation(pagerElem, NextPageTemplate);
            }

            if (!String.IsNullOrEmpty(PreviousGroupTemplate))
            {
                pagerElem.PreviousGroupTemplate = TransformationHelper.LoadTransformation(pagerElem, PreviousGroupTemplate);
            }

            if (!String.IsNullOrEmpty(NextGroupTemplate))
            {
                pagerElem.NextGroupTemplate = TransformationHelper.LoadTransformation(pagerElem, NextGroupTemplate);
            }

            if (!String.IsNullOrEmpty(DirectPageTemplate))
            {
                pagerElem.DirectPageTemplate = TransformationHelper.LoadTransformation(pagerElem, DirectPageTemplate);
            }

            if (!String.IsNullOrEmpty(LayoutTemplate))
            {
                pagerElem.LayoutTemplate = TransformationHelper.LoadTransformation(pagerElem, LayoutTemplate);
            }

            #endregion


            // Connects repeater with data source
            repLibraries.DataSource = srcLib.DataSource;
        }

        pagerElem.RebindPager();
        repLibraries.DataBind();
    }
}