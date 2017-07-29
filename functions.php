<?php
/**
 * Проверка ip на принадлежность диапазону
 *
 * @param $ip
 * @param $cidr_ranges
 *
 * @return bool
 */
function check_ip_in_range($ip, $cidr_ranges)
{
	// Check if given IP is inside a IP range with CIDR format
	$ip = ip2long($ip);
	if ( ! is_array($cidr_ranges)) {
		$cidr_ranges = array($cidr_ranges);
	}
	
	foreach ($cidr_ranges as $cidr_range) {
		list($subnet, $mask) = explode('/', $cidr_range);
		if (($ip & ~((1 << (32 - $mask)) - 1)) == ip2long($subnet)) {
			return true;
		}
	}
	
	return false;
}