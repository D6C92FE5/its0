
function HashPassword(statickey, $password, $hashed) {
    var password = $password.val();
    var hashed = CryptoJS.HmacSHA1(password, statickey).toString().toUpperCase();
    $hashed.val(hashed);
    password = password.replace(/./g, "*");
    $password.val(password);
}
