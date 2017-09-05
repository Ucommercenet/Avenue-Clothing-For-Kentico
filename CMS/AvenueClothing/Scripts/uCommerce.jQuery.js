var uCommerce = uCommerce || {};

$( "#checkHideShipping" ).change( function() {
    if (this.checked) {
        $("#shippingDiv").addClass("hide");
        $("#btnBillingAndShippingUpdate").addClass("hide");
        $("#btnBillingUpdate").removeClass("hide");
        $("#fixit").removeClass("span6");
        $("#fixit").addClass("span12");
    }
    else {
        $("#shippingDiv").removeClass("hide");
        $("#btnBillingAndShippingUpdate").removeClass("hide");
        $("#btnBillingUpdate").addClass("hide");
        $("#fixit").removeClass("span12");
        $("#fixit").addClass("span6");
        
    }
} );

(function ($) {


    $.extend({
        uCommerce : {
            getBasket: function(options, onSuccess, onError) {
                var defaults = {
//                    create: false
                };
                var extendedOptions = $.extend(defaults, options);
                callServiceStack({ GetBasket: extendedOptions }, onSuccess, onError);
            },
            getProductVariations: function(options, onSuccess, onError) {
                var defaults = {};
                var extendedOptions = $.extend(defaults, options);
                callServiceStack({ GetProductVariations: extendedOptions }, onSuccess, onError);
            },
            getVariantSkuFromSelection: function(options, onSuccess, onError) {
                var defaults = {};
                var extendedOptions = $.extend(defaults, options);
                callServiceStack({ GetVariantSkuFromSelection: extendedOptions }, onSuccess, onError);
            },
            search: function(options, onSuccess, onError) {
                var defaults = { };
                var extendedOptions = $.extend(defaults, options);
                callServiceStack({ Search: extendedOptions }, onSuccess, onError);
            },
            addToBasket: function(options, onSuccess, onError) {
                 var defaults = {
                    quantity: 1, 
                    sku: '',
                    variantSku: '',
                    addToExistingLine: true
                };
                var extendedOptions = $.extend(defaults, options);
                callServiceStack({ AddToBasket: extendedOptions }, onSuccess, onError);
            },
            updateLineItem: function(options, onSuccess, onError) {
                 var defaults = {
                    orderLineId: 0, 
                    newQuantity: 1
                };
                var extendedOptions = $.extend(defaults, options);
                callServiceStack({ UpdateLineItem: extendedOptions }, onSuccess, onError);
            }
        }
    });
    $.uCommerce.defaults = {
        servicepath: '/ucommerceapi',
        protocol: location.protocol,
        host: location.host
    };
    function callServiceStack(request, onSuccess, onError) {
        var gateway = new servicestack.ClientGateway($.uCommerce.defaults.protocol + "//" + $.uCommerce.defaults.host + $.uCommerce.defaults.servicepath + '/');
        gateway.postToService(request, onSuccess, onError);
    }
})(jQuery);


