function getVirtualAppPath() {
    var path = '/' + location.pathname.split('/')[1];
    if (path === "/CMSModules") path = "";

    return path;
}

// Using "parent" object to reference the main page, that hosts this iframe.
var virtualAppPath = getVirtualAppPath();

var constants = {
    baseurl: '',
    webPageBaseUrl: virtualAppPath + '/CMSModules/uCommerce/',
    serviceBaseUrl: virtualAppPath + '/ucommerceapi/'
};

var UCommerceClientMgr = {
    BaseUCommerceUrl: constants.webPageBaseUrl,
    Shell: 'Kentico'
}