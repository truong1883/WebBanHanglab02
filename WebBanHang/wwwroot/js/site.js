﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    showQuantiyCart();
});


$(".addtocart").click(function (evt) {
    evt.preventDefault();
    let id = $(this).attr("data-productId");

    $.ajax({
        url: "/customer/cart/addtocartapi",
        data: { "productId": id },
        success: function (data) {          
            Swal.fire({
                title: "Product added to cart",
                text: "You clicked the button!",
                icon: "success"
            });           
            showQuantiyCart();
        }
    });
})


let showQuantiyCart = () => {
    $.ajax({
        url: "/customer/cart/GetQuantityOfCart",
        success: function (data) {         
            $(".showcart").text(data.qty);
        }
    });
}