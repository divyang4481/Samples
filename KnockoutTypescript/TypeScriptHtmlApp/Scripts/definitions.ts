/// <reference path="typings/jquery/jquery.d.ts" />

function setContent(value: string) {
    $("#content").text(value);
}

$(() => {
    setContent("dsalkdjasdlkajdaslk");
});