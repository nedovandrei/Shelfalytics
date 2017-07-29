<?php
require_once 'config.php';
require_once 'deploy.php';
require_once 'functions.php';


// ip с которого пришел запрос
$ip = $_SERVER['HTTP_X_REAL_IP'];

// протокол запроса
$protocol = (isset($_SERVER['SERVER_PROTOCOL']) ? $_SERVER['SERVER_PROTOCOL'] : 'HTTP/1.0');

// Валидация ip запроса
if (empty($ip) || ! check_ip_in_range($ip, $bitbucket_IP_ranges)) {
	header($protocol . ' 400 Bad Request');
	die('invalid ip address.');
}

// Проверяем, чтобы ветка была правильной
$body = json_decode(file_get_contents('php://input'));

if ( ! $body) {
	header($protocol . ' 400 Bad Request');
	die('missing payload');
}

$push_branch = $body->push->changes[count($body->push->changes) - 1]->new->name;

if ($push_branch != $need_branch) {
	header($protocol . ' 200 OK');
	die('not required branch.');
}

// Выполняем пулл на сервер
$depl = new Deploy($path_repo);
$depl->set_log_file($log_file);

$depl->execute();